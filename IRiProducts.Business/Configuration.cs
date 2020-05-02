using Microsoft.Extensions.DependencyInjection;
using IRiProducts.Business.Models.Settings;
using IRiProducts.Business.Services;
using Microsoft.Extensions.Configuration;

namespace IRiProducts.Business
{
    public static class Configuration
    {
        public static void ConfigureBusinessServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Options
            services.Configure<RetailerProductSettings>(configuration.GetSection(nameof(RetailerProductSettings)));
            services.Configure<IRiProductSettings>(configuration.GetSection(nameof(IRiProductSettings)));

            // Services
            services.AddScoped<ICsvParserService, CsvParserService>();
            services.AddScoped<IRetailerProductsService, RetailerProductsService>();
            services.AddScoped<IIRiProductsService, IRiProductsService>();
        }
    }
}