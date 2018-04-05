using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace Shopify_DB_WriterAPI.Models
{
    public partial class ProductVariant
    {
        [J("id")] public long Id { get; set; }
        [J("product_id")] public long ProductId { get; set; }
        [J("title")] public string Title { get; set; }
        [J("price")] public string Price { get; set; }
        [J("sku")] public string Sku { get; set; }
        [J("position")] public long Position { get; set; }
        [J("inventory_policy")] public string InventoryPolicy { get; set; }
        [J("compare_at_price")] public string CompareAtPrice { get; set; }
        [J("fulfillment_service")] public string FulfillmentService { get; set; }
        [J("inventory_management")] public string InventoryManagement { get; set; }
        [J("option1")] public string Option1 { get; set; }
        [J("option2")] public string Option2 { get; set; }
        [J("option3")] public string Option3 { get; set; }
        [J("created_at")] public System.DateTimeOffset CreatedAt { get; set; }
        [J("updated_at")] public System.DateTimeOffset UpdatedAt { get; set; }
        [J("taxable")] public bool Taxable { get; set; }
        [J("barcode")] public long Barcode { get; set; }
        [J("grams")] public long Grams { get; set; }
        [J("image_id")] public long ImageId { get; set; }
        [J("inventory_quantity")] public long InventoryQuantity { get; set; }
        [J("weight")] public long Weight { get; set; }
        [J("weight_unit")] public string WeightUnit { get; set; }
        [J("inventory_item_id")] public long InventoryItemId { get; set; }
        [J("old_inventory_quantity")] public long OldInventoryQuantity { get; set; }
        [J("requires_shipping")] public bool RequiresShipping { get; set; }
    }
}