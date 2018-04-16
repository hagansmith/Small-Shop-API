using System.Net;
using System.Net.Http;
using System.Web.Http;
using Shopify_DB_WriterAPI.Models;
using Shopify_DB_WriterAPI.Products;
using Shopify_DB_WriterAPI.Services;

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

        // GET api/products/
        [Route, HttpGet]
        public HttpResponseMessage Get()
        {
            var repo = new ProductsRepository();
            var results = repo.GetLowStock();
            return Request.CreateResponse(HttpStatusCode.OK, results);
        }

        // GET api/products/1234567890123
        [Route("{id}"), HttpGet]
        public HttpResponseMessage Get(string id)
        {
            var repo = new ProductsRepository();
            var product = repo.GetProductById(id);

            return Request.CreateResponse(HttpStatusCode.OK, product);
        }

        // POST api/products
        [Route, HttpPost]
        public HttpResponseMessage Post(Product product)
        {
            var repo = new ProductsRepository();
            var results = repo.Post(product);

            return results == 1 ? Request.CreateResponse(HttpStatusCode.Created) : Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "A product with that id already exists");
        }

        // POST api/products/list
        [Route("list"), HttpPost]
        public HttpResponseMessage Post(object products)
        {
            var parse = new ProductParser();
            var prods = parse.Parse(products);
            var postCount = 0;

            foreach (Product product in prods)
            {
                var repo = new ProductsRepository();
                repo.Post(product);
                postCount += 1;
            }

            return prods.Count == postCount ? Request.CreateResponse(HttpStatusCode.Created) : Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Could not process your order, try again later...");
        }
    }
}
