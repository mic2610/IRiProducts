using System.Diagnostics;
using System.IO;
using IRiProducts.Business.Services;
using IRiProducts.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IRiProducts.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRetailerProductsService _retailerProductsService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(ILogger<HomeController> logger, IRetailerProductsService retailerProductsService, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _retailerProductsService = retailerProductsService;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var data = Path.Combine(_webHostEnvironment.WebRootPath, "data");
            var directory = System.IO.Directory.CreateDirectory(data);
            var filePath = Path.Combine(directory.FullName, "RetailerProducts.txt");
            var retailerProducts = _retailerProductsService.GetRetailerProducts(filePath);
            return View(retailerProducts);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
