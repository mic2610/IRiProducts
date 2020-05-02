using Microsoft.Extensions.DependencyInjection;
using IRiProducts.Business.Models.Settings;
using IRiProducts.Business.Services;
using IRiProducts.Business.Utilities;
using Microsoft.Extensions.Configuration;

namespace IRiProducts.Business
{
    public static class Configuration
    {
        public static void ConfigureBusinessServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Services
            services.AddScoped<ICsvParserService, CsvParserService>();
            services.AddScoped<IRetailerProductsService, RetailerProductsService>();
            services.AddScoped<IIRiProductsService, IRiProductsService>();

            // Utilities
            services.AddScoped<IProductUtility, ProductUtility>();
        }
    }
}