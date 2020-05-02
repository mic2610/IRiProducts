using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using IRiProducts.Business.CsvMappings.Csv;
using IRiProducts.Business.Models.Csv;

namespace IRiProducts.Business.Services
{
    public class IRiProductsService : IIRiProductsService
    {
        private readonly ICsvParserService _csvParserService;

        public IRiProductsService(ICsvParserService csvParserService)
        {
            _csvParserService = csvParserService;
        }

        public IList<IRiProduct> GetIRiProducts(string path)
        {
            return !string.IsNullOrWhiteSpace(path) ? _csvParserService.GetRecords<IRiProduct, IRiProductMapping>(path) : null;
        }
    }
}