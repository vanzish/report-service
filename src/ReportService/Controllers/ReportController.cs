using Microsoft.AspNetCore.Mvc;
using ReportService.Business.Managers;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ReportService.Controllers
{
    [Route("api/[controller]")]
    public class ReportController : Controller
    {
        private readonly ReportManager _reportManager;

        public ReportController(ReportManager reportManager)
        {
            _reportManager = reportManager;
        }

        [HttpGet]
        [Route("{year}/{month}")]
        public async Task<HttpResponseMessage> GetReport(int year, int month)
        {
            var reportString = await _reportManager.BuildReport(year, month);

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(reportString)
            };

            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            result.Content.Headers.ContentDisposition.FileName = "report.txt";
            return result;
        }
    }
}