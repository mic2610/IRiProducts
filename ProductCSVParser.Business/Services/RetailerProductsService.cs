using System.Collections.Generic;
using ProductCSVParser.Business.CsvMappings.Csv;
using ProductCSVParser.Business.Models.Csv;

namespace ProductCSVParser.Business.Services
{
    public class RetailerProductsService : IRetailerProductsService
    {
        private readonly ICsvParserService _csvParserService;

        public RetailerProductsService(ICsvParserService csvParserService)
        {
            _csvParserService = csvParserService;
        }

        public IList<CsvRetailerProduct> GetRetailerProducts(string path)
        {
            return !string.IsNullOrWhiteSpace(path) ? _csvParserService.GetRecords<CsvRetailerProduct, RetailProductMapping>(path) : null;
        }
    }
}