using System.Net;
using System.Net.Http;
using System.Web.Http;
using Shopify_DB_WriterAPI.Models;
using Shopify_DB_WriterAPI.Products;

namespace Shopify_DB_WriterAPI.Controllers
{
    public class ProductsController : ApiController
    {
        // GET api/LineItems
        public HttpResponseMessage Get()
        {
            //var items = new GetLineItems();
            //var results = items.getProducts();
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // GET api/LineItems/5
        public string Get(int id)
        {
            return id.ToString();
        }

        // POST api/Product
        public HttpResponseMessage Post(Product product)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            //var lineItemModel = lineItem.ToModel();
 
            //var prod = new ProductHelper();
            //var parsed = prod.Parse(product);

           // var productRoot = parsed.root;


            //foreach (LineItem item in items)
           // {
                var post = new PostProduct();
                var results = post.InsertProduct(product);
                //postCount += 1;
            //}

            if (results == 1)
                return Request.CreateResponse(HttpStatusCode.Created);

            return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "A product with that id already exists");
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
