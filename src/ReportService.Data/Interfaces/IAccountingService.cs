using System.Threading.Tasks;

namespace ReportService.Data.Interfaces
{
    public interface IAccountingService
    {
        Task<decimal> GetSalaryAsync(string employeeCode, string inn);
    }
}