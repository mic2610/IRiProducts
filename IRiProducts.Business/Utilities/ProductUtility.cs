using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IRiProducts.Business.Models;
using IRiProducts.Business.Models.Csv;
using IRiProducts.Core.Extensions;

namespace IRiProducts.Business.Utilities
{
    public class ProductUtility : IProductUtility
    {
        public IList<Product> FilterByCodeType(IList<RetailerProduct> retailerProducts, IList<IRiProduct> iriProducts)
        {
            if (retailerProducts.IsNullOrEmpty() || iriProducts.IsNullOrEmpty())
                return null;

            var iriProductLookup = iriProducts?.ToDictionary(k => k.Id);
            var products = new List<Product>();

            // Iterate through product ids groupings
            foreach (var retailProductGroup in retailerProducts.GroupBy(rp => rp.Id))
            {
                var productName = iriProductLookup.TryGetValue(retailProductGroup.Key, out var iriProduct) ? iriProduct.Name : "Product Name not available";

                // Iterate through product code type groupings
                foreach (var retailerProductCodeTypeGroup in retailProductGroup.GroupBy(rp => rp.RetailerProductCodeType))
                {
                    // Use MaxBy
                    // Get the latest by date from the product code type grouping
                    var retailerProduct = retailerProductCodeTypeGroup.MaxBy(rp => rp.DateReceived);
                    var product = BuildProduct(retailerProduct, productName);
                    if (product == null)
                        continue;

                    products.Add(product);
                }
            }

            return products;
        }

        private static Product BuildProduct(RetailerProduct retailerProduct, string name)
        {
            if (retailerProduct == null)
                return null;

            return new Product
            {
                Code = retailerProduct.RetailerProductCode,
                CodeType = retailerProduct.RetailerProductCodeType,
                Id = retailerProduct.Id,
                Name = name,
                DateReceived = retailerProduct.DateReceived
            };
        }
    }
}