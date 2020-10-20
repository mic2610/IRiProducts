using System.Collections.Generic;
using ProductCSVParser.Business.Models;
using ProductCSVParser.Business.Models.Csv;

namespace ProductCSVParser.Business.Services
{
    public interface IProductsService
    {
        IList<CsvProduct> GetProducts(string path);
    }
}