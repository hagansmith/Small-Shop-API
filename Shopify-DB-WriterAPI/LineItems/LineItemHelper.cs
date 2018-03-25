using Newtonsoft.Json.Linq;
using Shopify_DB_WriterAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shopify_DB_WriterAPI.LineItems
{
    public class LineItemHelper
    {
        public List<LineItem> Parse(object info)
        {
            JObject parsedObject = JObject.Parse(info.ToString());
            //get line items into list
            IList<JToken> results = parsedObject["line_items"].Children().ToList();
            //Serialize results into objects
            IList<LineItem> lineItems = new List<LineItem>();
            foreach (JToken result in results)
            {
                //JToken.ToObject helper method
                LineItem lineItem = result.ToObject<LineItem>();
                lineItems.Add(lineItem);
            }
            //Receive JSON and verify signature matches
            //Parse JSON to models
            //Verify DATA against DTO
            //Write data into DB
            return lineItems.ToList();
        }
    }
}