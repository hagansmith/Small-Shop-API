using Shopify_DB_WriterAPI.Dto;
using Shopify_DB_WriterAPI.LineItems;
using Shopify_DB_WriterAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Shopify_DB_WriterAPI.Products;

namespace Shopify_DB_WriterAPI.Controllers
{
    public class LineItemsController : ApiController
    {
        // GET api/LineItems
        public HttpResponseMessage Get()
        {
            var items = new GetLineItems();
            var results = items.getProducts();
            return Request.CreateResponse(HttpStatusCode.OK, results);
        }

        // GET api/LineItems/5
        public string Get(int id)
        {
            return id.ToString(); 
        }

        // POST api/LineItems
        public HttpResponseMessage Post(object order)
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
                var post = new PostLineItem();
                post.InsertLineItem(item);

                var adjustInventory = new AdjustProductCount();
                adjustInventory.DecrementProductCount(item.VariantId, item.Quantity);

                postCount +=1;
            }

            return items.Count == postCount ? Request.CreateResponse(HttpStatusCode.Created) : Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Could not process your order, try again later...");
        }

        // PUT api/LineItems/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/LineItems/5
        public void Delete(int id)
        {
        }
    }
}
