using AutoMapper;
using ProductCSVParser.Web.Models;

namespace ProductCSVParser.Web.Mappings
{
    public class RetailerProductMapping : Profile
    {
        public RetailerProductMapping()
        {
            CreateMap<Business.Models.Csv.CsvRetailerProduct, RetailerProduct>();
        }
    }
}