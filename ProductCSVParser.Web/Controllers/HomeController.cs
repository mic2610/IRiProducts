using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using AutoMapper;
using ProductCSVParser.Business.Models.Settings;
using ProductCSVParser.Business.Services;
using ProductCSVParser.Business.Utilities;
using ProductCSVParser.Core.Extensions;
using ProductCSVParser.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ProductCSVParser.Web.Controllers
{
    public class HomeController : Controller
    {
        // Services
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IRetailerProductsService _retailerProductsService;
        private readonly IProductsService _productsService;
        private readonly IMapper _mapper;

        // Utilities
        private readonly IProductUtility _productUtility;

        // Options
        private readonly RetailerProductSettings _retailerProductSettings;
        private readonly ProductSettings _productSettings;

        public HomeController(IWebHostEnvironment webHostEnvironment, IRetailerProductsService retailerProductsService, IProductsService iRiProductsService, IOptions<RetailerProductSettings> retailerProductSettings, IOptions<ProductSettings> iRiProductSettings, IProductUtility productUtility, IMapper mapper)
        {
            _webHostEnvironment = webHostEnvironment;
            _retailerProductsService = retailerProductsService;
            _productsService = iRiProductsService;
            _productUtility = productUtility;
            _mapper = mapper;
            _retailerProductSettings = retailerProductSettings.Value;
            _productSettings = iRiProductSettings.Value;
        }

        public IActionResult Index()
        {
            var products = _productUtility.FilterByCodeType(GetRetailerProducts(), GetCsvProducts());
            if (products == null)
                return NotFound();

            return View(_mapper.Map<IList<Business.Models.Product>, IList<Models.Product>>(products));
        }

        public IActionResult RetailerProducts()
        {
            var retailerProducts = GetRetailerProducts();
            if (retailerProducts.IsNullOrEmpty())
                return NotFound();

            return View(_mapper.Map<IList<Business.Models.Csv.CsvRetailerProduct>, IList<Models.RetailerProduct>>(retailerProducts));
        }

        public IActionResult CsvProducts()
        {
            var products = GetCsvProducts();
            if (products.IsNullOrEmpty())
                return NotFound();

            return View(_mapper.Map<IList<Business.Models.Csv.CsvProduct>, IList<Models.CsvProduct>>(products));

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private IList<Business.Models.Csv.CsvRetailerProduct> GetRetailerProducts()
        {
            var filePath = GetFilePath(_retailerProductSettings.DirectoryName, _retailerProductSettings.FileName);
            return _retailerProductsService.GetRetailerProducts(filePath);
        }

        private IList<Business.Models.Csv.CsvProduct> GetCsvProducts()
        {
            var filePath = GetFilePath(_productSettings.DirectoryName, _productSettings.FileName);
            return _productsService.GetProducts(filePath);
        }

        private string GetFilePath(string directoryName, string fileName)
        {
            var data = Path.Combine(_webHostEnvironment.WebRootPath, directoryName);
            var directory = new DirectoryInfo(data);
            return Path.Combine(directory.FullName, fileName);
        }
    }
}