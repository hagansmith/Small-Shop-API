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
    public class PostLineItem
    {
        readonly string _connectionString = ConfigurationManager.ConnectionStrings["Small_Shop"].ConnectionString;

        public int InsertLineItem(LineItem lineItem)
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