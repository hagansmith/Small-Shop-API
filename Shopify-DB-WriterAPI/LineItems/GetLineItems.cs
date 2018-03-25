using Shopify_DB_WriterAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;

namespace Shopify_DB_WriterAPI.LineItems
{
    public class GetLineItems
    {
        //readonly string _connectionString = ConfigurationManager.ConnectionStrings["Small_Shop"].ConnectionString;

        public List<LineItem> getProducts()
        {
            using (var db = new SqlConnection(ConfigurationManager.ConnectionStrings["Small_Shop"].ConnectionString))
            {
                db.Open();
                var lineItems =  
                    db.Query<LineItem>( @"SELECT 
                                           [id]
                                          ,[variant_id]
                                          ,[title]
                                          ,[quantity]
                                          ,[price]
                                          ,[sku]
                                          ,[variant_title]
                                          ,[vendor]
                                          ,[fulfillment_service]
                                          ,[product_id]
                                          ,[requires_shipping]
                                          ,[taxable]
                                          ,[gift_card]
                                          ,[name]
                                          ,[variant_inventory_management]
                                    FROM [dbo].[LineItem]");   
                return lineItems.ToList();
            }
        }
    }
}