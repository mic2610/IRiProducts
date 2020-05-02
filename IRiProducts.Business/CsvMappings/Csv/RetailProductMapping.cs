using CsvHelper.Configuration;
using IRiProducts.Business.Models;
using IRiProducts.Business.Models.Csv;
using IRiProducts.Core.Extensions;

namespace IRiProducts.Business.CsvMappings.Csv
{
    public class RetailProductMapping : ClassMap<RetailerProduct>
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