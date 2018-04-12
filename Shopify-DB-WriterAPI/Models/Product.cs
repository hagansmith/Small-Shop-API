using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace Shopify_DB_WriterAPI.Models
{
    public partial class Product
    {
        [J("id")] public long Id { get; set; }
        [J("title")] public string Title { get; set; }
        [J("body_html")] public string BodyHtml { get; set; }
        [J("vendor")] public string Vendor { get; set; }
        [J("product_type")] public string ProductType { get; set; }
        [J("created_at")] public System.DateTimeOffset CreatedAt { get; set; }
        [J("handle")] public string Handle { get; set; }
        [J("updated_at")] public System.DateTimeOffset UpdatedAt { get; set; }
        [J("published_at")] public System.DateTimeOffset PublishedAt { get; set; }
        [J("template_suffix")] public string TemplateSuffix { get; set; }
        [J("published_scope")] public string PublishedScope { get; set; }
        [J("tags")] public string Tags { get; set; }
        [J("variants")] public List<ProductVariant> Variants { get; set; }
        [J("options")] public List<ProductOption> Options { get; set; }
        [J("images")] public List<ProductImage> Images { get; set; }
        [J("image")] public ProductImage Image { get; set; }
    }

    //public partial class Product
    //{
    //    public static Product FromJson(string json) => JsonConvert.DeserializeObject<Product>(json, QuickType.Converter.Settings);
    //}

    //public static class Serialize
    //{
    //    public static string ToJson(this Product self) => JsonConvert.SerializeObject(self, QuickType.Converter.Settings);
    //}

    //internal class Converter
    //{
    //    public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
    //    {
    //        MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
    //        DateParseHandling = DateParseHandling.None,
    //        Converters = {
    //        new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
    //    },
    //    };
    //}
}
