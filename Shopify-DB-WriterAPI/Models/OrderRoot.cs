using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace Shopify_DB_WriterAPI.Models
{
    public partial class OrderRoot
    {
        [J("id")] public long Id { get; set; }
        [J("email")] public string Email { get; set; }
        [J("closed_at")] public object ClosedAt { get; set; }
        [J("created_at")] public System.DateTimeOffset CreatedAt { get; set; }
        [J("updated_at")] public System.DateTimeOffset UpdatedAt { get; set; }
        [J("number")] public long Number { get; set; }
        [J("note")] public object Note { get; set; }
        [J("token")] public string Token { get; set; }
        [J("gateway")] public object Gateway { get; set; }
        [J("test")] public bool Test { get; set; }
        [J("total_price")] public string TotalPrice { get; set; }
        [J("subtotal_price")] public string SubtotalPrice { get; set; }
        [J("total_weight")] public long TotalWeight { get; set; }
        [J("total_tax")] public string TotalTax { get; set; }
        [J("taxes_included")] public bool TaxesIncluded { get; set; }
        [J("currency")] public string Currency { get; set; }
        [J("financial_status")] public string FinancialStatus { get; set; }
        [J("confirmed")] public bool Confirmed { get; set; }
        [J("total_discounts")] public string TotalDiscounts { get; set; }
        [J("total_line_items_price")] public string TotalLineItemsPrice { get; set; }
        [J("cart_token")] public object CartToken { get; set; }
        [J("buyer_accepts_marketing")] public bool BuyerAcceptsMarketing { get; set; }
        [J("name")] public string Name { get; set; }
        [J("referring_site")] public object ReferringSite { get; set; }
        [J("landing_site")] public object LandingSite { get; set; }
        [J("cancelled_at")] public System.DateTimeOffset CancelledAt { get; set; }
        [J("cancel_reason")] public string CancelReason { get; set; }
        [J("total_price_usd")] public object TotalPriceUsd { get; set; }
        [J("checkout_token")] public object CheckoutToken { get; set; }
        [J("reference")] public object Reference { get; set; }
        [J("user_id")] public object UserId { get; set; }
        [J("location_id")] public object LocationId { get; set; }
        [J("source_identifier")] public object SourceIdentifier { get; set; }
        [J("source_url")] public object SourceUrl { get; set; }
        [J("processed_at")] public object ProcessedAt { get; set; }
        [J("device_id")] public object DeviceId { get; set; }
        [J("phone")] public object Phone { get; set; }
        [J("customer_locale")] public string CustomerLocale { get; set; }
        [J("app_id")] public object AppId { get; set; }
        [J("browser_ip")] public object BrowserIp { get; set; }
        [J("landing_site_ref")] public object LandingSiteRef { get; set; }
        [J("order_number")] public long OrderNumber { get; set; }
        [J("discount_codes")] public List<object> DiscountCodes { get; set; }
        [J("note_attributes")] public List<object> NoteAttributes { get; set; }
        [J("payment_gateway_names")] public List<string> PaymentGatewayNames { get; set; }
        [J("processing_method")] public string ProcessingMethod { get; set; }
        [J("checkout_id")] public object CheckoutId { get; set; }
        [J("source_name")] public string SourceName { get; set; }
        [J("fulfillment_status")] public string FulfillmentStatus { get; set; }
        [J("tax_lines")] public List<object> TaxLines { get; set; }
        [J("tags")] public string Tags { get; set; }
        [J("contact_email")] public string ContactEmail { get; set; }
        [J("order_status_url")] public string OrderStatusUrl { get; set; }
        [J("line_items")] public List<LineItem> LineItems { get; set; }
        [J("shipping_lines")] public List<ShippingLine> ShippingLines { get; set; }
        [J("billing_address")] public Address BillingAddress { get; set; }
        [J("shipping_address")] public Address ShippingAddress { get; set; }
        [J("fulfillments")] public List<object> Fulfillments { get; set; }
        [J("refunds")] public List<object> Refunds { get; set; }
        [J("customer")] public Customer Customer { get; set; }
    }

    //public partial class OrderRoot
    //{
    //    public static OrderRoot FromJson(string json) => JsonConvert.DeserializeObject<OrderRoot>(json, Converter.Settings);
    //}

    //internal class Converter
    //{
    //    public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
    //    {
    //        MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
    //        DateParseHandling = DateParseHandling.None,
    //        Converters = {
    //        new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
    //        },
    //    };
    //}
}