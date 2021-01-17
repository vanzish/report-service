using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReportService.Data.Interfaces;
using ReportService.Data.Services;
using RestSharp;

namespace ReportService.Extensions
{
    public static class AccountingClientExtension
    {
        public static void AddAccountingClient(this IServiceCollection services, IConfiguration configuration)
        {
            var url = configuration["RestClients:AccountingService"];
            var client = new RestClient(url) { ThrowOnAnyError = true };
            var accountingService = new AccountingService(client);

            services.AddSingleton<IAccountingService>(accountingService);
        }
    }
}