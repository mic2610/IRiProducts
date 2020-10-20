using AutoMapper;
using ProductCSVParser.Web.Models;

namespace ProductCSVParser.Web.Mappings
{
    public class ProductMapping : Profile
    {
        public ProductMapping()
        {
            CreateMap<Business.Models.Product, Product>();
        }
    }
}