using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace Shopify_DB_WriterAPI.Models
{
    public partial class ProductOption
    {
        [J("id")] public long Id { get; set; }
        [J("product_id")] public object ProductId { get; set; }
        [J("name")] public string Name { get; set; }
        [J("position")] public long Position { get; set; }
        [J("values")] public List<string> Values { get; set; }
    }
}