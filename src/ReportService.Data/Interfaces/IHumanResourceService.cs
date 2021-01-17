using System.Threading.Tasks;

namespace ReportService.Data.Interfaces
{
    public interface IHumanResourceService
    {
        Task<string> GetEmployeeCodeAsync(string inn);
    }
}