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
        public int updateVariant(LowInventoryDto product)
        {
            using (var db = new SqlConnection(ConfigurationManager.ConnectionStrings["Small_Shop"].ConnectionString))
            {
                product.ReorderDate = DateTime.Now;
                db.Open();
                var lines = db.Execute(@"UPDATE [dbo].[ProductVariant]
                                          SET [orderedInventoryQty] = @orderedInventoryQty
                                             ,[reorderDate] = @reorderDate
                                          WHERE ProductVariant.sku = @sku", product);
                return lines;
            }
        }
    }
}