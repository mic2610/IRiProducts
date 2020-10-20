using System.Collections.Generic;
using ProductCSVParser.Business.Models;
using ProductCSVParser.Business.Models.Csv;

namespace ProductCSVParser.Business.Utilities
{
    public interface IProductUtility
    {
        IList<Product> FilterByCodeType(IList<CsvRetailerProduct> retailerProducts, IList<CsvProduct> csvProducts);
    }
}