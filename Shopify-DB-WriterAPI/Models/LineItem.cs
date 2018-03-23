using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shopify_DB_WriterAPI.Models
{
    public class LineItem
    {
        public Int64 id { get; set; }
        public Int64 variant_id { get; set; }
        public string title { get; set; }
        public int quantity { get; set; }
        public double price { get; set; }
        public string sku { get; set; }
        public string variant_title { get; set; }
        public string vendor { get; set; }
        public string fulfillment_service { get; set; }
        public Int64 product_id { get; set; }
        public bool requires_shipping { get; set; }
        public bool taxable { get; set; }
        public bool gift_card { get; set; }
        public string name { get; set; }
        public bool variant_inventory_management { get; set; }
    }
}