using Newtonsoft.Json.Linq;
using Shopify_DB_WriterAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shopify_DB_WriterAPI.LineItems
{
    // Take the full order payload object and parse the line_items object out for use in inventory tracking.
    public class LineItemHelper
    {
        public List<LineItem> Parse(object info)
        {
            JObject parsedObject = JObject.Parse(info.ToString());
            //get line items into list
            IList<JToken> items = parsedObject["line_items"].Children().ToList();
            //Serialize results into objects
            IList<LineItem> lineItems = new List<LineItem>();
            foreach (JToken item in items)
            {
                //ToObject helper method part of newtonsoft JToken
                LineItem lineItem = item.ToObject<LineItem>();
                lineItems.Add(lineItem);
            }
            return lineItems.ToList();
        }
    }
}