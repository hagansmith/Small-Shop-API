using Shopify_DB_WriterAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shopify_DB_WriterAPI.Dto
{
    public class LineItemDto
    {
        [Required]
        public Int64 id { get; set; }

        public Int64 variant_id { get; set; }

        [Required, MaxLength(50, ErrorMessage = "The Title cannon be larger than 50 characters.")]
        public string title { get; set; }

        [Required]
        public int quantity { get; set; }

        [Required]
        public double price { get; set; }

        [Required, MaxLength(50, ErrorMessage = "The SKU cannon be larger than 50 characters.")]
        public string sku { get; set; }

        [MaxLength(50, ErrorMessage = "The Variant Title cannon be larger than 50 characters.")]
        public string variant_title { get; set; }

        [MaxLength(50, ErrorMessage = "The Vendor cannon be larger than 50 characters.")]
        public string vendor { get; set; }

        [MaxLength(50, ErrorMessage = "The Fulfillment Service cannon be larger than 50 characters.")]
        public string fulfillment_service { get; set; }

        [Required]
        public Int64 product_id { get; set; }

        public bool requires_shipping { get; set; }

        public bool taxable { get; set; }

        public bool gift_card { get; set; }

        [Required, MaxLength(100, ErrorMessage = "The Name cannon be larger than 100 characters.")]
        public string name { get; set; }

        public bool variant_inventory_management { get; set; }

        public LineItem ToModel()
        {
            return new LineItem()
            {
                id = id,
                variant_id = variant_id,
                title = title,
                quantity = quantity,
                price = price,
                sku = sku,
                variant_title = variant_title,
                vendor = vendor,
                fulfillment_service = fulfillment_service,
                product_id = product_id,
                requires_shipping = requires_shipping,
                taxable = taxable,
                gift_card  = gift_card,
                name = name,
                variant_inventory_management = variant_inventory_management,
            };
        }
    }
}