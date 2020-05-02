using System.Collections.Generic;
using IRiProducts.Business.CsvMappings.Csv;
using IRiProducts.Business.Models.Csv;

namespace IRiProducts.Business.Services
{
    public class RetailerProductsService : IRetailerProductsService
    {
        private readonly ICsvParserService _csvParserService;

        public RetailerProductsService(ICsvParserService csvParserService)
        {
            _csvParserService = csvParserService;
        }

        public IList<RetailerProduct> GetRetailerProducts(string path)
        {
            return !string.IsNullOrWhiteSpace(path) ? _csvParserService.GetRecords<RetailerProduct, RetailProductMapping>(path) : null;
        }
    }
}