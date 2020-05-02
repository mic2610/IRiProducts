using AutoMapper;
using IRiProducts.Web.Models;

namespace IRiProducts.Web.Mappings
{
    public class RetailerProductMapping : Profile
    {
        public RetailerProductMapping()
        {
            CreateMap<Business.Models.Csv.RetailerProduct, RetailerProduct>();
        }
    }
}