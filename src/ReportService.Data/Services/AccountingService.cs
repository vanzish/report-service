using ReportService.Data.Interfaces;
using RestSharp;
using System.Threading.Tasks;

namespace ReportService.Data.Services
{
    public class AccountingService : IAccountingService
    {
        private readonly IRestClient _restClient;

        public AccountingService(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<decimal> GetSalaryAsync(string employeeCode, string inn)
        {
            var request = new RestRequest($"/{inn}", Method.POST);
            var body = new { BuhCode = employeeCode };
            request.AddJsonBody(body);

            var result = await _restClient.ExecuteAsync<decimal>(request);

            return result.Data;
        }
    }
}