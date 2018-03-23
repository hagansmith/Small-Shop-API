using Shopify_DB_WriterAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Shopify_DB_WriterAPI.LineItems
{
    public class PostLineItem
    {
        readonly string _connectionString = ConfigurationManager.ConnectionStrings["Small_Shop"].ConnectionString;

        public bool InsertLineItem(LineItem lineItem)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"INSERT INTO [dbo].[LineItem]
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
                                               ,@variantInventoryManagement)";


                var IdParam = new SqlParameter("@id", System.Data.SqlDbType.BigInt);
                IdParam.Value = lineItem.id;
                cmd.Parameters.Add(IdParam);

                var VariantIdParam = new SqlParameter("@variantId", System.Data.SqlDbType.BigInt);
                VariantIdParam.Value = lineItem.variant_id;
                cmd.Parameters.Add(VariantIdParam);

                var TitleParam = new SqlParameter("@title", System.Data.SqlDbType.NVarChar);
                TitleParam.Value = lineItem.title;
                cmd.Parameters.Add(TitleParam);

                var QuantityParam = new SqlParameter("@quantity", System.Data.SqlDbType.Int);
                QuantityParam.Value = lineItem.quantity;
                cmd.Parameters.Add(QuantityParam);

                var PriceParam = new SqlParameter("@price", System.Data.SqlDbType.Money);
                PriceParam.Value = lineItem.price;
                cmd.Parameters.Add(PriceParam);

                var SkuParam = new SqlParameter("@sku", System.Data.SqlDbType.NVarChar);
                SkuParam.Value = lineItem.sku;
                cmd.Parameters.Add(SkuParam);

                var VariantTitleParam = new SqlParameter("@variantTitle", System.Data.SqlDbType.NVarChar);
                VariantTitleParam.Value = lineItem.variant_title;
                cmd.Parameters.Add(VariantTitleParam);

                var VendorParam = new SqlParameter("@vendor", System.Data.SqlDbType.NVarChar);
                VendorParam.Value = lineItem.vendor;
                cmd.Parameters.Add(VendorParam);

                var FulfillmentServiceParam = new SqlParameter("@fulfillmentService", System.Data.SqlDbType.NChar);
                FulfillmentServiceParam.Value = lineItem.fulfillment_service;
                cmd.Parameters.Add(FulfillmentServiceParam);

                var productIdParam = new SqlParameter("@productId", System.Data.SqlDbType.BigInt);
                productIdParam.Value = lineItem.product_id;
                cmd.Parameters.Add(productIdParam);

                var RequiresShippingParam = new SqlParameter("@requiresShipping", System.Data.SqlDbType.Bit);
                RequiresShippingParam.Value = lineItem.requires_shipping;
                cmd.Parameters.Add(RequiresShippingParam);

                var TaxableParam = new SqlParameter("@taxable", System.Data.SqlDbType.Bit);
                TaxableParam.Value = lineItem.taxable;
                cmd.Parameters.Add(TaxableParam);

                var GiftCardParam = new SqlParameter("@giftCard", System.Data.SqlDbType.Bit);
                GiftCardParam.Value = lineItem.gift_card;
                cmd.Parameters.Add(GiftCardParam);

                var NameParam = new SqlParameter("@name", System.Data.SqlDbType.NVarChar);
                NameParam.Value = lineItem.name;
                cmd.Parameters.Add(NameParam);

                var VarInventoryManagementParam = new SqlParameter("@variantInventoryManagement", System.Data.SqlDbType.Bit);
                VarInventoryManagementParam.Value = lineItem.variant_inventory_management;
                cmd.Parameters.Add(VarInventoryManagementParam);

                connection.Open();

                var result = cmd.ExecuteNonQuery();

                return result == 1;

            }
        }
    }
}