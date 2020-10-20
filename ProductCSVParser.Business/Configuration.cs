using Microsoft.Extensions.DependencyInjection;
using ProductCSVParser.Business.Services;
using ProductCSVParser.Business.Utilities;
using Microsoft.Extensions.Configuration;

namespace ProductCSVParser.Business
{
    public static class Configuration
    {
        public static void ConfigureBusinessServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Services
            services.AddScoped<ICsvParserService, CsvParserService>();
            services.AddScoped<IRetailerProductsService, RetailerProductsService>();
            services.AddScoped<IProductsService, ProductsService>();

            // Utilities
            services.AddScoped<IProductUtility, ProductUtility>();
        }
    }
}