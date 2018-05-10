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

        public List<ProductVariant> Get()
        {
            using (var db = new SqlConnection(_connectionString))
            {
                db.Open();
                var products = db.Query<ProductVariant>(@"USE [Small-Shop-Dev]
                                                        SELECT [variantId]
                                                              ,[productId]
                                                              ,[price]
                                                              ,[sku]
                                                              ,ProductVariant.[created]
                                                              ,ProductVariant.[updated]
                                                              ,[imageId]
                                                              ,[inventory_quantity]
                                                              ,[weight]
                                                              ,[requiresShipping]
                                                              ,[oldInventoryQty]
                                                              ,[allocatedInventoryQty]
                                                              ,[minimumStock]
	                                                          ,Product.title as title
                                                          FROM [Small-Shop-Dev].[dbo].[ProductVariant]
                                                          JOIN Product on ProductVariant.productId = Product.id
                                                          ORDER BY title");
                return products.ToList();
            }
        }

        public List<InventoryDto> GetLowStock()
        {
            using (var db = new SqlConnection(_connectionString))
            {
                db.Open();
                var products = db.Query<InventoryDto>(@"USE [Small-Shop-Dev]
                                            SELECT p.title, i.src image, v.sku, v.inventory_quantity remaining, v.variantId
                                              FROM [dbo].[Product] p
                                              JOIN dbo.ProductVariant v on p.id = v.productId
                                              JOIN dbo.ProductImage i on p.id = i.productId
                                              WHERE v.inventory_quantity <= v.minimumStock and v.sku <> ''");
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
                                                                      ,[inventory_quantity]
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
                                                           ,[inventory_quantity] -= @quantity
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
                                                            ,@reorderDate)", new { variantId, count, reorderDate });
                return lines;
            }
        }

        public List<InventoryDto> GetProductsOnReorder()
        {
            using (var db = new SqlConnection(_connectionString))
            {
                db.Open();
                var products = db.Query<InventoryDto>(@"USE [Small-Shop-Dev]
                                                        SELECT p.title, i.src image, v.sku, v.inventory_quantity remaining, v.variantId, r.quantity orderedInventoryQty, r.orderDate reorderDate, r.quantityReceived, r.id
                                                        FROM dbo.Reorder r
                                                        JOIN dbo.ProductVariant v on r.variantId = v.variantId
											            JOIN dbo.Product p on v.productId = p.id
                                                        JOIN dbo.ProductImage i on p.Id = i.productId
                                                        WHERE r.quantityReceived < r.quantity OR r.quantityReceived IS NULL");
                return products.ToList();
            }
        }

        public int Receive(int id, int count)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var date = DateTime.Now;
                db.Open();
                var result = db.Execute(@"UPDATE [dbo].[Reorder]
                                             SET [dateRecieved] = @date
                                                ,[quantityReceived] = @count
                                           WHERE Id = @id", new {id, count, date});
                return result;
            } 
        }

        public int Edit(ProductVariant product)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                db.Open();
                var result = db.Execute(@"UPDATE [dbo].[ProductVariant]
                                               SET [title] = @title
                                                  ,[price] = @price
                                                  ,[sku] = @sku
                                                  ,[created] = @created
                                                  ,[updated] = @updated
                                                  ,[inventoryQty] = @inventoryQty
                                                  ,[weight] = @weight
                                                  ,[minimumStock] = @minimumStock
                                             WHERE variantId = @variantId", product);

                return result;
            }
        }

        public int PatchCount(long id, int count)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                db.Open();
                var result = db.Execute(@"UPDATE [dbo].[ProductVariant]
                                             SET [inventory_quantity] = @count
                                         WHERE variantId = @id", new {id, count});

                return result;
            }
        }

        public int Delete(long id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                  var productRowsAffected = connection.Execute(@" DELETE FROM[dbo].[Product]
                                                                        WHERE id = @id", new { id });

                        
                  return productRowsAffected;
            }
            
        }
    }
}