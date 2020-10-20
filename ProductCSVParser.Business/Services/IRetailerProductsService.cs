using System.Collections.Generic;
using ProductCSVParser.Business.Models.Csv;

namespace ProductCSVParser.Business.Services
{
    public interface IRetailerProductsService
    {
        IList<CsvRetailerProduct> GetRetailerProducts(string path);
    }
}