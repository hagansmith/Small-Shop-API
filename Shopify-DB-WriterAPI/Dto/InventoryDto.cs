using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using Newtonsoft.Json;
using Shopify_DB_WriterAPI.Models;

namespace Shopify_DB_WriterAPI.Dto
{
    public class InventoryDto
    {
        [JsonProperty("id")] public int Id { get; set; }
        [JsonProperty("variantId")] public string VariantId { get; set; }
        [JsonProperty("sku")] public string Sku { get; set; }
        [JsonProperty("title")] public string Title { get; set; }
        [JsonProperty("image")] public string Image { get; set; }
        [JsonProperty("remaining")] public string Remaining { get; set; }
        [JsonProperty("reorderDate")] public DateTime ReorderDate { get; set; }
        [JsonProperty("orderedInventoryQty")] public int OrderedInventoryQty { get; set; }

    }
}