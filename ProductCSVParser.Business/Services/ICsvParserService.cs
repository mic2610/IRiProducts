using System.Collections.Generic;
using CsvHelper.Configuration;

namespace ProductCSVParser.Business.Services
{
    public interface ICsvParserService
    {
        IList<TModel> GetRecords<TModel, TCsvMap>(string path) where TModel : class where TCsvMap : ClassMap;
    }
}