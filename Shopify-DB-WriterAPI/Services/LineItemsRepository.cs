using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using Shopify_DB_WriterAPI.Models;

namespace Shopify_DB_WriterAPI.Services
{
    public class LineItemsRepository
    {
        readonly string _connectionString = ConfigurationManager.ConnectionStrings["Small_Shop"].ConnectionString;

        public List<LineItem> Get()
        {
            using (var db = new SqlConnection(_connectionString))
            {
                db.Open();
                var lineItems =
                    db.Query<LineItem>(@"SELECT 
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

        public int Post(LineItem lineItem)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var lines = connection.Execute(@"INSERT INTO [dbo].[LineItem]
                                               ([id]
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
                                               ,[variant_inventory_management])
                                         VALUES
                                               (@id
                                               ,@variantId
                                               ,@title
                                               ,@quantity
                                               ,@price
                                               ,@sku
                                               ,@variantTitle
                                               ,@vendor
                                               ,@fulfillmentService
                                               ,@productId
                                               ,@requiresShipping
                                               ,@taxable
                                               ,@giftCard
                                               ,@name
                                               ,@variantInventoryManagement)", lineItem);

                return lines;
            }
        }
    }

}