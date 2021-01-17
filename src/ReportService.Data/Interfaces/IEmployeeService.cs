using ReportService.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReportService.Data.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetEmployees();
    }
}