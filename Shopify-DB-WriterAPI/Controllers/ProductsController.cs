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
        [Route("{id}"), HttpGet]
        public HttpResponseMessage Get(string id)
        {
            var repo = new GetProducts();
            var product = repo.GetProductById(id);
            return Request.CreateResponse(HttpStatusCode.OK, product);
        }

        // POST api/products
        [Route, HttpPost]
        public HttpResponseMessage Post(Product product)
        {

            var post = new PostProduct();
            var results = post.InsertProduct(product);

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
                var post = new PostProduct();
                post.InsertProduct(product);
                postCount += 1;
            }

            return prods.Count == postCount ? Request.CreateResponse(HttpStatusCode.Created) : Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Could not process your order, try again later...");
        }

        // PATCH api/products/sku/
        [Route("{sku}/{count}"), HttpPatch]
        public HttpResponseMessage OnOrderUpdater(string sku, int count)
        {
            var patch = new PatchProduct();
            var results = patch.updateVariant(sku, count);

            return results == 1 ? Request.CreateResponse(HttpStatusCode.Created) : Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Unable to process request");
        }

        //// DELETE api/LineItems/5
        //public void Delete(int id)
        //{
        //}
    }
}
