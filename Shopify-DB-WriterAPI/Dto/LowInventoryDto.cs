using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Shopify_DB_WriterAPI.Models;

namespace Shopify_DB_WriterAPI.Dto
{
    public class LowInventoryDto
    {
        [JsonProperty("sku")] public string Sku { get; set; }
        [JsonProperty("title")] public string Title { get; set; }
        [JsonProperty("image")] public string Image { get; set; }
    }
}