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
using Microsoft.Extensions.Logging;

namespace IRiProducts.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<HomeController> _logger;
        private readonly IRetailerProductsService _retailerProductsService;
        private readonly IIRiProductsService _iRiProductsService;

        public HomeController(IWebHostEnvironment webHostEnvironment, ILogger<HomeController> logger, IRetailerProductsService retailerProductsService, IIRiProductsService iRiProductsService)
        {
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
            _retailerProductsService = retailerProductsService;
            _iRiProductsService = iRiProductsService;
        }

        public IActionResult Index()
        {
            var retailerProducts = GetRetailerProducts()?.ToList();
            if (retailerProducts.IsNullOrEmpty())
                return NotFound();

            var iriProductLookup = GetIRiProducts()?.ToDictionary(k => k.Id);
            if (iriProductLookup.IsNullOrEmpty())
                return NotFound();

            var products = new List<Product>();
            var groupedRetailerProducts = retailerProducts.OrderByDescending(rp => rp.DateReceived).GroupBy(rp => rp.RetailerProductCode);
            foreach (var groupedRetailerProduct in groupedRetailerProducts)
            {
                foreach (var retailerProduct in groupedRetailerProduct)
                {
                    products.Add(new Product
                    {
                        Code = retailerProduct.RetailerProductCode,
                        CodeType = retailerProduct.RetailerProductCodeType,
                        Id = retailerProduct.Id,
                        Name = iriProductLookup.TryGetValue(retailerProduct.Id, out var iriProduct) ? iriProduct.Name : "Product Name not available",
                        DateReceived = retailerProduct.DateReceived
                    });
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

        private IEnumerable<RetailerProduct> GetRetailerProducts()
        {
            var filePath = GetFilePath("Data", "RetailerProducts.txt");
            return _retailerProductsService.GetRetailerProducts(filePath);
        }

        private IEnumerable<IriProduct> GetIRiProducts()
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
