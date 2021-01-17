using ReportService.Data.Interfaces;
using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Business.Managers
{
    public class ReportManager
    {
        private readonly IEmployeeService _employeeService;
        private const string Separator = "--------------------------------------------";

        public ReportManager(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public async Task<string> BuildReport(int year, int month)
        {
            var sb = new StringBuilder();
            var yearTitle = new DateTime(year, month, 1).ToString("MMMMMM yyyy", CultureInfo.CurrentCulture);

            sb.AppendLine(yearTitle)
              .AppendLine()
              .AppendLine(Separator);

            var employees = await _employeeService.GetEmployees();

            var employeeDict = employees.GroupBy(x => x.Department).Select(x => new
            {
                DepartmentName = x.Key,
                Employees = x.ToList()
            });

            foreach (var department in employeeDict)
            {
                sb.AppendLine()
                  .AppendLine(department.DepartmentName)
                  .AppendLine();

                foreach (var employee in department.Employees)
                {
                    sb.AppendLine($"{employee.Name} {employee.Salary:#.##р}")
                      .AppendLine();
                }

                var departmentSalarySum = department.Employees.Sum(x => x.Salary);

                sb.AppendLine(string.Format(Resources.Report.DepartmentSum, departmentSalarySum))
                  .AppendLine();

                sb.AppendLine(Separator);
            }

            var totalSalariesSum = employees.Sum(x => x.Salary);
            sb.AppendLine(string.Format(Resources.Report.TotalSum, totalSalariesSum));

            return sb.ToString();
        }
    }
}