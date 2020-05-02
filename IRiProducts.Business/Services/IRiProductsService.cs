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
        public IList<IriProduct> GetIriProducts(string path)
        {
            using var reader = new StreamReader(path);
            using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csvReader.Configuration.RegisterClassMap<IRiProductMapping>();
                return csvReader.GetRecords<IriProduct>()?.ToList();
            }
        }
    }
}