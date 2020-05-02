using System.Collections.Generic;
using IRiProducts.Business.Models.Csv;

namespace IRiProducts.Business.Services
{
    public interface IIRiProductsService
    {
        IList<IRiProduct> GetIRiProducts(string path);
    }
}