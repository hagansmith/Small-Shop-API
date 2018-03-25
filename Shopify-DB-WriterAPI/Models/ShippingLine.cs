using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace Shopify_DB_WriterAPI.Models
{
    public partial class ShippingLine
    {
        [J("id")] public long Id { get; set; }
        [J("title")] public string Title { get; set; }
        [J("price")] public string Price { get; set; }
        [J("code")] public object Code { get; set; }
        [J("source")] public string Source { get; set; }
        [J("phone")] public object Phone { get; set; }
        [J("requested_fulfillment_service_id")] public object RequestedFulfillmentServiceId { get; set; }
        [J("delivery_category")] public object DeliveryCategory { get; set; }
        [J("carrier_identifier")] public object CarrierIdentifier { get; set; }
        [J("discounted_price")] public string DiscountedPrice { get; set; }
        [J("tax_lines")] public List<object> TaxLines { get; set; }
    }
}