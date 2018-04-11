using System.Net;
using System.Net.Http;
using System.Web.Http;
using Shopify_DB_WriterAPI.Models;
using Shopify_DB_WriterAPI.Products;

namespace Shopify_DB_WriterAPI.Controllers
{
    [RoutePrefix("api/products")]
    public class ProductsController : ApiController
    {
        //// GET api/products
        //[Route, HttpGet]
        //public HttpResponseMessage Get()
        //{
        //    var products = new GetProducts();
        //    var results = products.getProducts();
        //    return Request.CreateResponse(HttpStatusCode.OK, results);
        //}

        // GET api/products/variants
        [Route, HttpGet]
        public HttpResponseMessage Get()
        {
            var repo = new GetProducts();
            var results = repo.GetLowStock();
            return Request.CreateResponse(HttpStatusCode.OK, results);
        }

        // GET api/products/5
        public string Get(int id)
        {
            return id.ToString();
        }

        // POST api/products
        [Route, HttpPost]
        public HttpResponseMessage Post(Product product)
        {

            var post = new PostProduct();
            var results = post.InsertProduct(product);

            return results == 1 ? Request.CreateResponse(HttpStatusCode.Created) : Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "A product with that id already exists");
        }

        //// PUT api/LineItems/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/LineItems/5
        //public void Delete(int id)
        //{
        //}
    }
}
