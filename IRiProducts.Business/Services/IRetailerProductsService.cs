using System.Collections.Generic;
using IRiProducts.Business.Models;

namespace IRiProducts.Business.Services
{
    public interface IRetailerProductsService
    {
        IList<RetailerProduct> GetRetailerProducts(string path);
    }
}