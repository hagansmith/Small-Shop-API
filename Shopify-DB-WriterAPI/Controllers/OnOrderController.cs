using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Shopify_DB_WriterAPI.Models;
using Shopify_DB_WriterAPI.Products;
using Shopify_DB_WriterAPI.Services;

namespace Shopify_DB_WriterAPI.Controllers
{
    [RoutePrefix("api/onOrder")]
    public class OnOrderController : ApiController
    {
        // GET api/onOrder
        [Route, HttpGet]
        public HttpResponseMessage Get()
        {
            var repo = new ProductsRepository();
            var results = repo.GetProductsOnReorder();
            return Request.CreateResponse(HttpStatusCode.OK, results);
        }

        // POST api/onOrder/1234567890123/500
        [Route("{sku}/{count}"), HttpPost]
        public HttpResponseMessage OnOrderUpdater(string sku, int count)
        {
            var repo = new ProductsRepository();
            var results = repo.CreateReorder(sku, count);
            return results == 1 ? Request.CreateResponse(HttpStatusCode.Created) : Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Unable to process request");
        }
    }
}
