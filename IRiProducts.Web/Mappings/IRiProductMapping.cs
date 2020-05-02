using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
