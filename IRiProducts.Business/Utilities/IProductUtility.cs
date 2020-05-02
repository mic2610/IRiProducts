using System.Collections.Generic;
using IRiProducts.Business.Models;
using IRiProducts.Business.Models.Csv;

namespace IRiProducts.Business.Utilities
{
    public interface IProductUtility
    {
        IList<Product> FilterByCodeType(IList<RetailerProduct> retailerProducts, IList<IRiProduct> iriProducts);
    }
}