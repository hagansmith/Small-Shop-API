using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using Shopify_DB_WriterAPI.Dto;
using Shopify_DB_WriterAPI.Models;

namespace Shopify_DB_WriterAPI.Products
{
    public class OnOrder
    {
        //public List<Product> getProducts()
        //{
        //    using (var db = new SqlConnection(ConfigurationManager.ConnectionStrings["Small_Shop"].ConnectionString))
        //    {
        //        db.Open();
        //        var products = db.Query<Product>(@"USE [Small-Shop-Dev]
        //                                SELECT[id]
        //                                    ,[title]
        //                                    ,[vendor]
        //                                    ,[type]
        //                                    ,[created]
        //                                    ,[updated]
        //                                    ,[published]
        //                                    ,[imageId]
        //                                FROM[dbo].[Product]");
        //        return products.ToList();
        //    }
        //}



        //public LowInventoryDto GetProductById(string id)
        //{
        //    using (var db = new SqlConnection(ConfigurationManager.ConnectionStrings["Small_Shop"].ConnectionString))
        //    {
        //        db.Open();
        //        var product = db.QueryFirst<LowInventoryDto>(@"SELECT  [title] 
        //                                                              ,[sku]
        //                                                              ,[imageId]
        //                                                              ,[inventoryQty] 
        //                                                              ,[orderedInventoryQty]
        //                                                              ,[reorderDate]
        //                                                          FROM [dbo].[ProductVariant]
        //                                                          WHERE ProductVariant.sku = @id", new { id });
        //        return product;
        //    }
        //}
    }
} 