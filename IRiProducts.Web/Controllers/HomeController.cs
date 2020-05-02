using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using IRiProducts.Business.Models;
using IRiProducts.Business.Models.Csv;
using IRiProducts.Business.Models.Settings;
using IRiProducts.Business.Services;
using IRiProducts.Core.Extensions;
using IRiProducts.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace IRiProducts.Web.Controllers
{
    public class HomeController : Controller
    {
        // Services
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IRetailerProductsService _retailerProductsService;
        private readonly IIRiProductsService _iRiProductsService;

        // Options
        private readonly RetailerProductSettings _retailerProductSettings;
        private readonly IRiProductSettings _iRiProductSettings;

        public HomeController(IWebHostEnvironment webHostEnvironment, IRetailerProductsService retailerProductsService, IIRiProductsService iRiProductsService, IOptions<RetailerProductSettings> retailerProductSettings, IOptions<IRiProductSettings> iRiProductSettings)
        {
            _webHostEnvironment = webHostEnvironment;
            _retailerProductsService = retailerProductsService;
            _iRiProductsService = iRiProductsService;
            _retailerProductSettings = retailerProductSettings.Value;
            _iRiProductSettings = iRiProductSettings.Value;
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
                    // Use MaxBy
                    // Get the latest by date from the product code type grouping
                    var retailerProduct = retailerProductCodeTypeGroup.MaxBy(rp => rp.DateReceived);
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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
            var filePath = GetFilePath(_retailerProductSettings.DirectoryName, _retailerProductSettings.FileName);
            return _retailerProductsService.GetRetailerProducts(filePath);
        }

        private IList<IriProduct> GetIRiProducts()
        {
            var filePath = GetFilePath(_iRiProductSettings.DirectoryName, _iRiProductSettings.FileName);
            return _iRiProductsService.GetIriProducts(filePath);
        }

        private string GetFilePath(string directoryName, string fileName)
        {
            var data = Path.Combine(_webHostEnvironment.WebRootPath, directoryName);
            var directory = new DirectoryInfo(data);
            return Path.Combine(directory.FullName, fileName);
        }
    }
}
