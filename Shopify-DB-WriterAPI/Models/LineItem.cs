using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace Shopify_DB_WriterAPI.Models
{
    public partial class LineItem
    {
        [J("id")] public long Id { get; set; }
        [J("variant_id")] public string VariantId { get; set; }
        [J("title")] public string Title { get; set; }
        [J("quantity")] public int Quantity { get; set; }
        [J("price")] public string Price { get; set; }
        [J("sku")] public string Sku { get; set; }
        [J("variant_title")] public string VariantTitle { get; set; }
        [J("vendor")] public string Vendor { get; set; }
        [J("fulfillment_service")] public string FulfillmentService { get; set; }
        [J("product_id")] public long ProductId { get; set; }
        [J("requires_shipping")] public bool RequiresShipping { get; set; }
        [J("taxable")] public bool Taxable { get; set; }
        [J("gift_card")] public bool GiftCard { get; set; }
        [J("name")] public string Name { get; set; }
        [J("variant_inventory_management")] public string VariantInventoryManagement { get; set; }
        [J("properties")] public List<object> Properties { get; set; }
        [J("product_exists")] public bool ProductExists { get; set; }
        [J("fulfillable_quantity")] public int FulfillableQuantity { get; set; }
        [J("grams")] public long Grams { get; set; }
        [J("total_discount")] public string TotalDiscount { get; set; }
        [J("fulfillment_status")] public string FulfillmentStatus { get; set; }
        [J("tax_lines")] public List<object> TaxLines { get; set; }
    }
}