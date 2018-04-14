using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Shopify_DB_WriterAPI.Dto;
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

        // PATCH api/products/sku
        [Route("{sku}"), HttpPatch]
        public HttpResponseMessage OnOrderUpdater(LowInventoryDto product)
        {
            var patch = new PatchProduct();
            var results = patch.updateVariant(product);

            return results == 2 ? Request.CreateResponse(HttpStatusCode.Created) : Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Unable to process request");
        }

        //// DELETE api/LineItems/5
        //public void Delete(int id)
        //{
        //}
    }
}
