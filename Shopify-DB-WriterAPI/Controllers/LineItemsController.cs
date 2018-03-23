using Shopify_DB_WriterAPI.Dto;
using Shopify_DB_WriterAPI.LineItems;
using Shopify_DB_WriterAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Shopify_DB_WriterAPI.Controllers
{
    public class LineItemsController : ApiController
    {
        //private PostLineItem _postLineItem = null;

        //public LineItemsController()
        //{

        //}

        //public LineItemsController(PostLineItem postLineItem)
        //{
        //    _postLineItem = postLineItem;
        //}

        // GET api/LineItems
        public List<Models.LineItem> Get()
        {
            var items = new GetLineItems();
            var results = items.getProducts();
            return results;
        }

        // GET api/LineItems/5
        public string Get(int id)
        {
            return id.ToString(); 
        }

        // POST api/LineItems
        public IHttpActionResult Post(LineItemDto lineItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var lineItemModel = lineItem.ToModel();
            var post = new PostLineItem();
            post.InsertLineItem(lineItemModel);

            lineItem.id = lineItemModel.id;

            return Created(
                Url.Link("DefaultApi", new { controller = "LineItems",  lineItem.id }), lineItem
                );
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
