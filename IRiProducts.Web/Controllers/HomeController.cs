using System.Diagnostics;
using IRiProducts.Business.Services;
using IRiProducts.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IRiProducts.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRetailerProductsService _retailerProductsService;

        public HomeController(ILogger<HomeController> logger, IRetailerProductsService retailerProductsService)
        {
            _logger = logger;
            _retailerProductsService = retailerProductsService;
        }

        public IActionResult Index()
        {
            var retailerProducts = _retailerProductsService.GetRetailerProducts();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
