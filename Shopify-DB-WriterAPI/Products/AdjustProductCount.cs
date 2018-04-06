using Newtonsoft.Json.Linq;
using Shopify_DB_WriterAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;

namespace Shopify_DB_WriterAPI.Products
{
    public class AdjustProductCount
    {
        //Adjust DB record by the line item quantity
        readonly string _connectionString = ConfigurationManager.ConnectionStrings["Small_Shop"].ConnectionString;

        public int DecrementProductCount(string variantId, int quantity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var lines = connection.Execute(@"UPDATE [dbo].[ProductVariant]
                                                        SET [updated] = GETDATE()
                                                           ,[inventoryQty] - @quantity
                                                        WHERE ProductVariant.Id = @variantId", new {variantId, quantity});
                return lines;
            }

        }
    }
}