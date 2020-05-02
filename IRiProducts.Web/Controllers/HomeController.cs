using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using IRiProducts.Business.Models;
using IRiProducts.Business.Models.Csv;
using IRiProducts.Business.Services;
using IRiProducts.Core.Extensions;
using IRiProducts.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace IRiProducts.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IRetailerProductsService _retailerProductsService;
        private readonly IIRiProductsService _iRiProductsService;

        public HomeController(IWebHostEnvironment webHostEnvironment, IRetailerProductsService retailerProductsService, IIRiProductsService iRiProductsService)
        {
            _webHostEnvironment = webHostEnvironment;
            _retailerProductsService = retailerProductsService;
            _iRiProductsService = iRiProductsService;
        }

        public IActionResult Index()
        {
            var retailProducts = GetRetailerProducts();
            if (retailProducts.IsNullOrEmpty())
                return null;

            var iriProductLookup = GetIRiProducts()?.ToDictionary(k => k.Id);
            if (iriProductLookup.IsNullOrEmpty())
                return null;

            var products = new List<Product>();

            // Iterate through product ids groupings
            foreach (var retailProductGroup in retailProducts.GroupBy(rp => rp.Id))
            {
                var productName = iriProductLookup.TryGetValue(retailProductGroup.Key, out var iriProduct) ? iriProduct.Name : "Product Name not available";

                // Iterate through product code type groupings
                foreach (var retailerProductCodeTypeGroup in retailProductGroup.GroupBy(rp => rp.RetailerProductCodeType))
                {
                    // Get the latest by date from the product code type grouping
                    var retailerProduct = retailerProductCodeTypeGroup.OrderByDescending(rp => rp.DateReceived).FirstOrDefault();
                    var product = BuildProduct(retailerProduct, productName);
                    if (product == null)
                        continue;

                    products.Add(product);
                }

            }

            return View(products);
        }

        public IActionResult RetailerProducts()
        {
            var retailerProducts = GetRetailerProducts();
            return View(retailerProducts);
        }

        public IActionResult IRiProducts()
        {
            var iRiProducts = GetIRiProducts();
            return View(iRiProducts);
        }

        private static Product BuildProduct(RetailerProduct retailerProduct, string name)
        {
            if (retailerProduct == null)
                return null;

            return new Product
            {
                Code = retailerProduct.RetailerProductCode,
                CodeType = retailerProduct.RetailerProductCodeType,
                Id = retailerProduct.Id,
                Name = name,
                DateReceived = retailerProduct.DateReceived
            };
        }

        private IList<RetailerProduct> GetRetailerProducts()
        {
            var filePath = GetFilePath("Data", "RetailerProducts.txt");
            return _retailerProductsService.GetRetailerProducts(filePath);
        }

        private IList<IriProduct> GetIRiProducts()
        {
            var filePath = GetFilePath("Data", "IRIProducts.txt");
            return _iRiProductsService.GetIriProducts(filePath);
        }

        private string GetFilePath(string directoryName, string fileName)
        {
            var data = Path.Combine(_webHostEnvironment.WebRootPath, directoryName);
            var directory = new DirectoryInfo(data);
            return Path.Combine(directory.FullName, fileName);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
