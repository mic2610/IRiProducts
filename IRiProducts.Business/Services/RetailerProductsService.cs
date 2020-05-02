using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using IRiProducts.Business.CsvMappings;
using IRiProducts.Business.Models;

namespace IRiProducts.Business.Services
{
    public class RetailerProductsService : IRetailerProductsService
    {
        public IList<RetailerProduct> GetRetailerProducts()
        {
            using var reader = new StreamReader(@"C:\dev\Challenges\IRiProducts\IRiProducts.Web\wwwroot\Data");
            using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csvReader.Configuration.RegisterClassMap<RetailProductMapping>();
                return csvReader.GetRecords<RetailerProduct>()?.ToList();
            }
        }
    }
}