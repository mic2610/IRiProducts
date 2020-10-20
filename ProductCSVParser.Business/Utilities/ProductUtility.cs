using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProductCSVParser.Business.Models;
using ProductCSVParser.Business.Models.Csv;
using ProductCSVParser.Core.Extensions;

namespace ProductCSVParser.Business.Utilities
{
    public class ProductUtility : IProductUtility
    {
        public IList<Product> FilterByCodeType(IList<CsvRetailerProduct> retailerProducts, IList<CsvProduct> csvProducts)
        {
            if (retailerProducts.IsNullOrEmpty() || csvProducts.IsNullOrEmpty())
                return null;

            var csvProductLookup = csvProducts?.ToDictionary(k => k.Id);
            var products = new List<Product>();

            // Iterate through product ids groupings
            foreach (var retailProductGroup in retailerProducts.GroupBy(rp => rp.Id))
            {
                // Return a safe failure if the product name is not available within the lookup
                var productName = csvProductLookup.TryGetValue(retailProductGroup.Key, out var iriProduct) ? iriProduct.Name : Constants.Product.NameNotAvailable;

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

        private static Product BuildProduct(CsvRetailerProduct retailerProduct, string name)
        {
            if (retailerProduct == null)
                return null;

            return new Product
            {
                Code = retailerProduct.RetailerProductCode,
                CodeType = retailerProduct.RetailerProductCodeType,
                Id = retailerProduct.Id,
                Name = name,
                RetailerName = retailerProduct.RetailerName,
                DateReceived = retailerProduct.DateReceived
            };
        }
    }
}