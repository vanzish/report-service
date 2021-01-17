using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReportService.Data.Interfaces;
using ReportService.Data.Services;
using RestSharp;

namespace ReportService.Extensions
{
    public static class HumanResourceClientExtension
    {
        public static void AddHumanResourceClient(this IServiceCollection services, IConfiguration configuration)
        {
            var url = configuration["RestClients:HrService"];
            var client = new RestClient(url) { ThrowOnAnyError = true };
            var hrService = new HumanResourceService(client);

            services.AddSingleton<IHumanResourceService>(hrService);
        }
    }
}