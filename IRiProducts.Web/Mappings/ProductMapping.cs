using AutoMapper;
using IRiProducts.Web.Models;

namespace IRiProducts.Web.Mappings
{
    public class ProductMapping : Profile
    {
        public ProductMapping()
        {
            CreateMap<Business.Models.Product, Product>();
        }
    }
}