using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using IRiProducts.Business.CsvMappings;
using IRiProducts.Business.CsvMappings.Csv;
using IRiProducts.Business.Models;
using IRiProducts.Business.Models.Csv;

namespace IRiProducts.Business.Services
{
    public class RetailerProductsService : IRetailerProductsService
    {
        public IList<RetailerProduct> GetRetailerProducts(string path)
        {
            using var reader = new StreamReader(path);
            using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csvReader.Configuration.RegisterClassMap<RetailProductMapping>();
                return csvReader.GetRecords<RetailerProduct>()?.ToList();
            }
        }
    }
}