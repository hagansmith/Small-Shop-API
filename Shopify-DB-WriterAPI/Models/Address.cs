using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace Shopify_DB_WriterAPI.Models
{
    public partial class Address
    {
        [J("first_name")] public string FirstName { get; set; }
        [J("address1")] public string Address1 { get; set; }
        [J("phone")] public string Phone { get; set; }
        [J("city")] public string City { get; set; }
        [J("zip")] public string Zip { get; set; }
        [J("province")] public string Province { get; set; }
        [J("country")] public string Country { get; set; }
        [J("last_name")] public string LastName { get; set; }
        [J("address2")] public object Address2 { get; set; }
        [J("company")] public string Company { get; set; }
        [J("latitude")] public object Latitude { get; set; }
        [J("longitude")] public object Longitude { get; set; }
        [J("name")] public string Name { get; set; }
        [J("country_code")] public string CountryCode { get; set; }
        [J("province_code")] public string ProvinceCode { get; set; }
        [J("id")] public long? Id { get; set; }
        [J("customer_id")] public long? CustomerId { get; set; }
        [J("country_name")] public string CountryName { get; set; }
        [J("default")] public bool? Default { get; set; }
    }
}