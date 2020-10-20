using CsvHelper.Configuration;
using ProductCSVParser.Business.Models.Csv;

namespace ProductCSVParser.Business.CsvMappings.Csv
{
    public class ProductMapping : ClassMap<CsvProduct>
    {
        public ProductMapping()
        {
            Map(m => m.Id).Index(0);
            Map(m => m.Name).Index(1);
        }
    }
}