using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace Shopify_DB_WriterAPI.Models
{
    public partial class ProductImage
    {
        [J("id")] public long Id { get; set; }
        [J("product_id")] public long ProductId { get; set; }
        [J("position")] public long Position { get; set; }
        [J("created_at")] public System.DateTimeOffset CreatedAt { get; set; }
        [J("updated_at")] public System.DateTimeOffset UpdatedAt { get; set; }
        [J("width")] public long Width { get; set; }
        [J("height")] public long Height { get; set; }
        [J("src")] public string Src { get; set; }
        [J("variant_ids")] public List<object> VariantIds { get; set; }
    }
}