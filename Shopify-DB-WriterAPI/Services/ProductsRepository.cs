using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Shopify_DB_WriterAPI.Dto;
using Shopify_DB_WriterAPI.Models;

namespace Shopify_DB_WriterAPI.Services
{
    public class ProductsRepository
    {
        readonly string _connectionString = ConfigurationManager.ConnectionStrings["Small_Shop"].ConnectionString;

        public int Post(Product newProduct)
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

        public List<Product> Get()
        {
            using (var db = new SqlConnection(_connectionString))
            {
                db.Open();
                var products = db.Query<Product>(@"USE [Small-Shop-Dev]
                                        SELECT[id]
                                            ,[title]
                                            ,[vendor]
                                            ,[type]
                                            ,[created]
                                            ,[updated]
                                            ,[published]
                                            ,[imageId]
                                        FROM[dbo].[Product]");
                return products.ToList();
            }
        }

        public List<InventoryDto> GetLowStock()
        {
            using (var db = new SqlConnection(_connectionString))
            {
                db.Open();
                var products = db.Query<InventoryDto>(@"USE [Small-Shop-Dev]
                                            SELECT p.title, i.src image, v.sku, v.inventoryQty remaining, v.variantId
                                              FROM [dbo].[Product] p
                                              JOIN dbo.ProductVariant v on p.id = v.productId
                                              JOIN dbo.ProductImage i on p.id = i.productId
                                              WHERE v.inventoryQty <= v.minimumStock and v.sku <> ''");
                return products.ToList();
            }
        }

        public InventoryDto GetProductById(string id)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                db.Open();
                var product = db.QueryFirst<InventoryDto>(@"SELECT  [title] 
                                                                      ,[sku]
                                                                      ,[imageId]
                                                                      ,[inventoryQty]
                                                                      ,[variantId]
                                                                  FROM [dbo].[ProductVariant]
                                                                  WHERE ProductVariant.sku = @id", new { id });
                return product;
            }
        }

        public int DecrementProductCount(string variantId, int quantity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var lines = connection.Execute(@"UPDATE [dbo].[ProductVariant]
                                                        SET [updated] = GETDATE()
                                                           ,[inventoryQty] -= @quantity
                                                        WHERE ProductVariant.variantId = @variantId", new {variantId, quantity});
                return lines;
            }

        }

        public int CreateReorder(string variantId, int count)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var reorderDate = DateTime.Now;
                db.Open();
                var lines = db.Execute(@"INSERT INTO [dbo].[Reorder]
                                                            ([variantId]
                                                            ,[quantity]
                                                            ,[orderDate])
                                                    VALUES
                                                            (@variantId
                                                            ,@count
                                                            ,@reorderDate", new { variantId, count, reorderDate });
                return lines;
            }
        }

        public List<InventoryDto> GetProductsOnReorder()
        {
            using (var db = new SqlConnection(_connectionString))
            {
                db.Open();
                var products = db.Query<InventoryDto>(@"USE [Small-Shop-Dev]
                                                               SELECT p.title, i.src image, v.sku, v.inventoryQty remaining, r.quantity orderedInventoryQty, r.orderDate reorderDate
                                                               FROM dbo.Reorder r
                                                               JOIN dbo.ProductVariant v on r.variantId = v.variantId
											                   Join dbo.Product p on v.productId = p.id
                                                               JOIN dbo.ProductImage i on p.Id = i.productId");
                return products.ToList();
            }
        }
    }
}