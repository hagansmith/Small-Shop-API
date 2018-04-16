using Shopify_DB_WriterAPI.Models;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Shopify_DB_WriterAPI.LineItems;
using Shopify_DB_WriterAPI.Products;
using Shopify_DB_WriterAPI.Services;

namespace Shopify_DB_WriterAPI.Controllers
{
    [RoutePrefix("api/lineItems")]
    public class LineItemsController : ApiController
    {
        // GET api/LineItems
        [Route, HttpGet]
        public HttpResponseMessage GetOrderLineItems()
        {
            var items = new LineItemsRepository();
            var results = items.Get();
            return Request.CreateResponse(HttpStatusCode.OK, results);
        }

        // POST api/LineItems
        [Route, HttpPost]
        public HttpResponseMessage PostOrderLineItems(object order)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            //var lineItemModel = lineItem.ToModel();
            var lineItems = new LineItemHelper();
            var items = lineItems.Parse(order);
            var postCount = 0;

            foreach (LineItem item in items)
            {
                var repo = new LineItemsRepository();
                repo.Post(item);

                var adjustInventory = new ProductsRepository();
                adjustInventory.DecrementProductCount(item.VariantId, item.Quantity);

                postCount +=1;
            }

            return items.Count == postCount ? Request.CreateResponse(HttpStatusCode.Created) : Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Could not process your order, try again later...");
        }
    }
}
