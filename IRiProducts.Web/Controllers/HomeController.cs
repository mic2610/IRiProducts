using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using IRiProducts.Business.Models;
using IRiProducts.Business.Models.Csv;
using IRiProducts.Business.Models.Settings;
using IRiProducts.Business.Services;
using IRiProducts.Business.Utilities;
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

        // Utilities
        private readonly IProductUtility _productUtility;

        // Options
        private readonly RetailerProductSettings _retailerProductSettings;
        private readonly IRiProductSettings _iRiProductSettings;

        public HomeController(IWebHostEnvironment webHostEnvironment, IRetailerProductsService retailerProductsService, IIRiProductsService iRiProductsService, IOptions<RetailerProductSettings> retailerProductSettings, IOptions<IRiProductSettings> iRiProductSettings, IProductUtility productUtility)
        {
            _webHostEnvironment = webHostEnvironment;
            _retailerProductsService = retailerProductsService;
            _iRiProductsService = iRiProductsService;
            _productUtility = productUtility;
            _retailerProductSettings = retailerProductSettings.Value;
            _iRiProductSettings = iRiProductSettings.Value;
        }

        public IActionResult Index()
        {
            var products = _productUtility.FilterByCodeType(GetRetailerProducts(), GetIRiProducts());
            if (products == null)
                return NotFound();

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

        private IList<RetailerProduct> GetRetailerProducts()
        {
            var filePath = GetFilePath(_retailerProductSettings.DirectoryName, _retailerProductSettings.FileName);
            return _retailerProductsService.GetRetailerProducts(filePath);
        }

        private IList<IRiProduct> GetIRiProducts()
        {
            var filePath = GetFilePath(_iRiProductSettings.DirectoryName, _iRiProductSettings.FileName);
            return _iRiProductsService.GetIRiProducts(filePath);
        }

        private string GetFilePath(string directoryName, string fileName)
        {
            var data = Path.Combine(_webHostEnvironment.WebRootPath, directoryName);
            var directory = new DirectoryInfo(data);
            return Path.Combine(directory.FullName, fileName);
        }
    }
}
