using ReportService.Data.Interfaces;
using RestSharp;
using System.Threading.Tasks;

namespace ReportService.Data.Services
{
    public class HumanResourceService : IHumanResourceService
    {
        private readonly IRestClient _restClient;

        public HumanResourceService(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<string> GetEmployeeCodeAsync(string inn)
        {
            var request = new RestRequest($"/{inn}", Method.GET);
            var result = await _restClient.ExecuteAsync<string>(request);

            return result.Data;
        }
    }
}