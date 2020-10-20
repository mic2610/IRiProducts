using AutoMapper;
using ProductCSVParser.Web.Models;

namespace ProductCSVParser.Web.Mappings
{
    public class CsvProductMapping : Profile
    {
        public CsvProductMapping()
        {
            CreateMap<Business.Models.Csv.CsvProduct, CsvProduct>();
        }
    }
}