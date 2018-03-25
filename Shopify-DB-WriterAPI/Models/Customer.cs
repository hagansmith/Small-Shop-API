using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace Shopify_DB_WriterAPI.Models
{
    public partial class Customer
    {
        [J("id")] public long Id { get; set; }
        [J("email")] public string Email { get; set; }
        [J("accepts_marketing")] public bool AcceptsMarketing { get; set; }
        [J("created_at")] public object CreatedAt { get; set; }
        [J("updated_at")] public object UpdatedAt { get; set; }
        [J("first_name")] public string FirstName { get; set; }
        [J("last_name")] public string LastName { get; set; }
        [J("orders_count")] public long OrdersCount { get; set; }
        [J("state")] public string State { get; set; }
        [J("total_spent")] public string TotalSpent { get; set; }
        [J("last_order_id")] public object LastOrderId { get; set; }
        [J("note")] public object Note { get; set; }
        [J("verified_email")] public bool VerifiedEmail { get; set; }
        [J("multipass_identifier")] public object MultipassIdentifier { get; set; }
        [J("tax_exempt")] public bool TaxExempt { get; set; }
        [J("phone")] public object Phone { get; set; }
        [J("tags")] public string Tags { get; set; }
        [J("last_order_name")] public object LastOrderName { get; set; }
        [J("default_address")] public Address DefaultAddress { get; set; }
    }
}