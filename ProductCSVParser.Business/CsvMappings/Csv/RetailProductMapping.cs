using CsvHelper.Configuration;
using ProductCSVParser.Business.Models;
using ProductCSVParser.Business.Models.Csv;
using ProductCSVParser.Core.Extensions;

namespace ProductCSVParser.Business.CsvMappings.Csv
{
    public class RetailProductMapping : ClassMap<CsvRetailerProduct>
    {
        public RetailProductMapping()
        {
            Map(m => m.Id).Index(0);
            Map(m => m.RetailerName).Index(1);
            Map(m => m.RetailerProductCode).Index(2);
            Map(m => m.RetailerProductCodeType).Index(3);
            Map(m => m.DateReceived).ConvertUsing(c => c.GetField(4).ToDateTime().GetValueOrDefault());
        }
    }
}