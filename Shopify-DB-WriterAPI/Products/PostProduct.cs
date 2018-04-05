using Dapper;
using Shopify_DB_WriterAPI.Models;
using System.Configuration;
using System.Data.SqlClient;

namespace Shopify_DB_WriterAPI.Products
{
    public class PostProduct
    {
        readonly string _connectionString = ConfigurationManager.ConnectionStrings["Small_Shop"].ConnectionString;

        public int InsertProduct (Product newProduct)
        {
            var images = newProduct.Images;
            var options = newProduct.Options;
            var variants = newProduct.Variants;
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var lines = connection.Execute(@"USE [Small-Shop-Dev]
                                                            INSERT INTO [dbo].[Product]
                                                                       ([id]
                                                                       ,[title]
                                                                       ,[vendor]
                                                                       ,[type]
                                                                       ,[created]
                                                                       ,[updated]
                                                                       ,[published]
                                                                       ,[imageId])
                                                                 VALUES
                                                                       (@id
                                                                       ,@title
                                                                       ,@vendor
                                                                       ,@producttype
                                                                       ,@createdat
                                                                       ,@updatedat
                                                                       ,@publishedat
                                                                       ,@image)", newProduct);
                var variantLines = 0;
                if (lines == 1)
                {
                    foreach (ProductVariant variant in variants)
                    {
                        variantLines += connection.Execute(@"USE [Small-Shop-Dev]
                                                            INSERT INTO [dbo].[ProductVariant]
                                                                       ([variantId]
                                                                       ,[productId]
                                                                       ,[title]
                                                                       ,[price]
                                                                       ,[sku]
                                                                       ,[created]
                                                                       ,[updated]
                                                                       ,[imageId]
                                                                       ,[inventoryQty]
                                                                       ,[weight]
                                                                       ,[requiresShipping])
                                                                 VALUES
                                                                       (@id
                                                                       ,@productId
                                                                       ,@title
                                                                       ,@price
                                                                       ,@sku
                                                                       ,@createdat
                                                                       ,@updatedat
                                                                       ,@imageId
                                                                       ,@inventoryQuantity
                                                                       ,@weight
                                                                       ,@requiresShipping)", variant);
                    }
                }

                else return 0;

                var imageLines = 0;
                if (variantLines == variants.Count)
                {
                    foreach (ProductImage image in images)
                    {
                        imageLines += connection.Execute(@"USE [Small-Shop-Dev]
                                                            INSERT INTO[dbo].[ProductImage]
                                                                        ([id]
                                                                        ,[productId]
                                                                        ,[created]
                                                                        ,[updated]
                                                                        ,[width]
                                                                        ,[height]
                                                                        ,[src])
                                                                    VALUES
                                                                        (@id
                                                                        ,@productId
                                                                        ,@created
                                                                        ,@updated
                                                                        ,@width
                                                                        ,@height
                                                                        ,@src)", image);
                    }
                }
                else return 0;

                if (imageLines == images.Count)
                    return 1;

                 return 0;
            }
        }
    }
}