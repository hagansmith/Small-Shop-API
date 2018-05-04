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
        // GET api/products/variants
        [Route("variants"), HttpGet]
        public HttpResponseMessage GetProducts()
        {
            var products = new ProductsRepository();
            var results = products.Get();
            return Request.CreateResponse(HttpStatusCode.OK, results);
        }

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

        // PUT api/products/1234567890123
        [Route("{id}"), HttpPut]
        public HttpResponseMessage Put(ProductVariant productVariant)
        {
            var repo = new ProductsRepository();
            var product = repo.Edit(productVariant);

            return Request.CreateResponse(HttpStatusCode.OK, product);
        }

        // PATCH api/products/{id}/{count}
        [Route("{id}/{count}"), HttpPatch]
        public HttpResponseMessage Patch(long id, int count)
        {
            var repo = new ProductsRepository();
            var update = repo.PatchCount(id, count);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // Delete api/products/id
        [Route("{id}"), HttpDelete]
        public HttpResponseMessage Delete(long id)
        {
            var repo = new ProductsRepository();
            var results = repo.Delete(id);

            return results == 1 ? Request.CreateResponse(HttpStatusCode.OK) : Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "A product with that id already exists");
        }
    }
}
