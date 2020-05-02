using CsvHelper.Configuration;
using IRiProducts.Business.Models.Csv;

namespace IRiProducts.Business.CsvMappings.Csv
{
    public class IRiProductMapping : ClassMap<IriProduct>
    {
        public IRiProductMapping()
        {
            Map(m => m.Id).Index(0);
            Map(m => m.Name).Index(1);
        }
    }
}