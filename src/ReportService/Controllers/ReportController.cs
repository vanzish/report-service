using Microsoft.AspNetCore.Mvc;
using ReportService.Business.Managers;
using ReportService.ViewModels;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace ReportService.Controllers
{
    [Route("api/[controller]")]
    public class ReportController : ApiController
    {
        private readonly ReportManager _reportManager;

        public ReportController(ReportManager reportManager)
        {
            _reportManager = reportManager;
        }

        [HttpGet]
        [Route("{year}/{month}")]
        public async Task<IActionResult> GetReport(ReportDate date)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reportString = await _reportManager.BuildReport(date.Year, date.Month);

            var content = Encoding.UTF8.GetBytes(reportString);

            var result = new FileContentResult(content, "application/octet-stream")
            {
                FileDownloadName = "report.txt"
            };

            return result;
        }
    }
}