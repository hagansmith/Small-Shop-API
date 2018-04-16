using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Shopify_DB_WriterAPI.Models;

namespace Shopify_DB_WriterAPI.Products
{
    public class ProductParser
    {
        public List<Product> Parse(object products)
        {
            JObject parsedObject = JObject.Parse(products.ToString());
            //get line items into list
            IList<JToken> prods = parsedObject["products"].Children().ToList();
            //Serialize results into objects
            IList<Product> allTheProducts = new List<Product>();
            foreach (JToken prod in prods)
            {
                //ToObject helper method part of newtonsoft JToken
                Product product = prod.ToObject<Product>();
                allTheProducts.Add(product);
            }

            return allTheProducts.ToList();
        }
    }
}