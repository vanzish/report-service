using Npgsql;
using ReportService.Data.Interfaces;
using ReportService.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ReportService.Data.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly string _connectionString;
        private readonly IAccountingService _accountingService;
        private readonly IHumanResourceService _humanResourceService;

        public EmployeeService(
            IAccountingService accountingService,
            IHumanResourceService humanResourceService,
            IConfiguration configuration)
        {
            _accountingService = accountingService;
            _humanResourceService = humanResourceService;
            _connectionString = configuration.GetConnectionString("EmployeeDatabaseConnection");
        }

        public async Task<List<Employee>> GetEmployees()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var cmd = new NpgsqlCommand(
                    "SELECT e.name, e.inn, d.name from emps e inner join deps d on e.departmentid = d.id where d.active = true",
                    connection))
                using (var reader = cmd.ExecuteReader())
                {
                    var employees = new List<Employee>();

                    while (reader.Read())
                    {
                        var employee = new Employee
                        {
                            Name = reader.GetString(0),
                            Inn = reader.GetString(1),
                            Department = reader.GetString(2)
                        };

                        employee.PersonalCode = await _humanResourceService.GetEmployeeCodeAsync(employee.Inn);
                        employee.Salary = await _accountingService.GetSalaryAsync(employee.PersonalCode, employee.Inn);
                        employees.Add(employee);
                    }

                    return employees;
                }
            }
        }
    }
}