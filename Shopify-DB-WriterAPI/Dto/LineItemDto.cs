using Shopify_DB_WriterAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace Shopify_DB_WriterAPI.Dto
{
    public class LineItemDto
    {
        [Required]
        public long Id { get; set; }

        public string Variant_id { get; set; }

        [Required, MaxLength(50, ErrorMessage = "The Title cannon be larger than 50 characters.")]
        public string Title { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public double Price { get; set; }

        [Required, MaxLength(50, ErrorMessage = "The SKU cannon be larger than 50 characters.")]
        public string Sku { get; set; }

        [MaxLength(50, ErrorMessage = "The Variant Title cannon be larger than 50 characters.")]
        public string Variant_title { get; set; }

        [MaxLength(50, ErrorMessage = "The Vendor cannon be larger than 50 characters.")]
        public string Vendor { get; set; }

        [MaxLength(50, ErrorMessage = "The Fulfillment Service cannon be larger than 50 characters.")]
        public string Fulfillment_service { get; set; }

        [Required]
        public long Product_id { get; set; }

        public bool Requires_shipping { get; set; }

        public bool Taxable { get; set; }

        public bool Gift_card { get; set; }

        [Required, MaxLength(100, ErrorMessage = "The Name cannon be larger than 100 characters.")]
        public string Name { get; set; }

        public string Variant_inventory_management { get; set; }

        public LineItem ToModel()
        {
            return new LineItem()
            {
                Id = Id,
                VariantId = Variant_id,
                Title = Title,
                Quantity = Quantity,
                Price = Price.ToString(),
                Sku = Sku,
                VariantTitle = Variant_title,
                Vendor = Vendor,
                FulfillmentService = Fulfillment_service,
                ProductId = Product_id,
                RequiresShipping = Requires_shipping,
                Taxable = Taxable,
                GiftCard = Gift_card,
                Name = Name,
                VariantInventoryManagement = Variant_inventory_management,
            };
        }
    }
}