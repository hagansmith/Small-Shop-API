using Shopify_DB_WriterAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Shopify_DB_WriterAPI.LineItems
{
    public class GetLineItems
    {
        readonly string _connectionString = ConfigurationManager.ConnectionStrings["Small_Shop"].ConnectionString;

        public List<LineItem> getProducts()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"SELECT 
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
                                    FROM [dbo].[LineItem]";

                var reader = cmd.ExecuteReader();

                List<LineItem> lineItems = new List<LineItem>();

                while (reader.Read())
                {
                    var lineItem = new LineItem
                    {
                        id = Int64.Parse(reader["id"].ToString()),
                        variant_id = Int64.Parse(reader["variant_id"].ToString()),
                        title = reader["title"].ToString(),
                        quantity = int.Parse(reader["quantity"].ToString()),
                        price = double.Parse(reader["price"].ToString()),
                        sku = reader["sku"].ToString(),
                        variant_title = reader["variant_title"].ToString(),
                        vendor = reader["vendor"].ToString(),
                        fulfillment_service = reader["fulfillment_service"].ToString(),
                        product_id = Int64.Parse(reader["product_id"].ToString()),
                        requires_shipping = bool.Parse(reader["requires_shipping"].ToString()),
                        taxable = bool.Parse(reader["taxable"].ToString()),
                        gift_card = bool.Parse(reader["gift_card"].ToString()),
                        name = reader["name"].ToString(), 
                        variant_inventory_management = bool.Parse(reader["variant_inventory_management"].ToString())
                    };
                    lineItems.Add(lineItem);
                }
                return lineItems;
            }
        }
    }
}