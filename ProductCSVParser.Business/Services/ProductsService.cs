using System.Collections.Generic;
using ProductCSVParser.Business.CsvMappings.Csv;
using ProductCSVParser.Business.Models.Csv;

namespace ProductCSVParser.Business.Services
{
    public class ProductsService : IProductsService
    {
        private readonly ICsvParserService _csvParserService;

        public ProductsService(ICsvParserService csvParserService)
        {
            _csvParserService = csvParserService;
        }

        public IList<CsvProduct> GetProducts(string path)
        {
            return !string.IsNullOrWhiteSpace(path) ? _csvParserService.GetRecords<CsvProduct, ProductMapping>(path) : null;
        }
    }
}