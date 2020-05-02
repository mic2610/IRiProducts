using AutoMapper;
using IRiProducts.Web.Models;

namespace IRiProducts.Web.Mappings
{
    public class IRiProductMapping : Profile
    {
        public IRiProductMapping()
        {
            CreateMap<Business.Models.Csv.IRiProduct, IRiProduct>();
        }
    }
}