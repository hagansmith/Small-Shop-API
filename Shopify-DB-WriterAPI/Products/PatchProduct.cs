using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using Shopify_DB_WriterAPI.Dto;

namespace Shopify_DB_WriterAPI.Products
{
    public class PatchProduct
    {
        public int updateVariant(string sku, int count)
        {
            using (var db = new SqlConnection(ConfigurationManager.ConnectionStrings["Small_Shop"].ConnectionString))
            {
                var reorderDate = DateTime.Now;
                db.Open();
                var lines = db.Execute(@"UPDATE [dbo].[ProductVariant]
                                          SET [orderedInventoryQty] += @count
                                             ,[reorderDate] = @reorderDate
                                          WHERE ProductVariant.sku = @sku", new {sku, count, reorderDate});
                return lines;
            }
        }
    }
}