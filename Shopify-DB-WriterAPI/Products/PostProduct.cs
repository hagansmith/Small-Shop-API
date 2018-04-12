using System;
using Dapper;
using Shopify_DB_WriterAPI.Models;
using System.Configuration;
using System.Data.SqlClient;

namespace Shopify_DB_WriterAPI.Products
{
    public class PostProduct
    {
        readonly string _connectionString = ConfigurationManager.ConnectionStrings["Small_Shop"].ConnectionString;

        public int InsertProduct(Product newProduct)
        {
            var images = newProduct.Images;
            var options = newProduct.Options;
            var variants = newProduct.Variants;
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                using (transaction)
                {
                    try
                    {
                        var productRowsAffected = connection.Execute(@"USE [Small-Shop-Dev]
                                                            INSERT INTO [dbo].[Product]
                                                                       ([id]
                                                                       ,[title]
                                                                       ,[vendor]
                                                                       ,[type]
                                                                       ,[created]
                                                                       ,[updated]
                                                                       ,[published]
                                                                       )
                                                                 VALUES
                                                                       (@id
                                                                       ,@title
                                                                       ,@vendor
                                                                       ,@producttype
                                                                       ,@createdat
                                                                       ,@updatedat
                                                                       ,@publishedat
                                                                       )", newProduct, transaction: transaction);

                        foreach (ProductVariant variant in variants)
                        {
                            connection.Execute(@"USE [Small-Shop-Dev]
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
                                                                       ,@requiresShipping)", variant, transaction: transaction);
                        }

                        foreach (ProductImage image in images)
                        {
                            connection.Execute(@"USE [Small-Shop-Dev]
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
                                                                        ,@createdAt
                                                                        ,@updatedAt
                                                                        ,@width
                                                                        ,@height
                                                                        ,@src)", image, transaction: transaction);
                        }

                        foreach (ProductOption option in options)
                        {
                            connection.Execute(@"USE [Small-Shop-Dev]
                                                           INSERT INTO[dbo].[Option]
                                                                            ([id]
                                                                            ,[productId]
                                                                            ,[name]
                                                                            ,[position]
                                                                            )
                                                                        VALUES
                                                                            (@id
                                                                            ,@productId
                                                                            ,@name
                                                                            ,@position
                                                                            )", option, transaction: transaction);
                        }
                        transaction.Commit();
                        return productRowsAffected;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        try
                        {
                            transaction.Rollback();
                            return 0;
                        }
                        catch (Exception exRollback)
                        {
                            Console.WriteLine(exRollback.Message);
                        }
                    }
                }
                return 0;
            }
        }
    }
}