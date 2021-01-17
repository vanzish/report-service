using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using ReportService.Business.Managers;
using ReportService.Business.Resources;
using ReportService.Data.Interfaces;
using ReportService.Entities.Models;

namespace ReportService.Tests.UnitTests
{
    [TestFixture]
    public class ReportTests
    {
        private ReportManager _reportManager;

        private List<Employee> employees = new List<Employee>
        {
            new Employee
            {
                Department = "ИТ",
                Name = "Сергеев Петр Иванович",
                Inn = "999999999999",
                Salary = 90000m,
                PersonalCode = "90-09239"
            },
            new Employee
            {
                Department = "ИТ",
                Name = "Иванов Евгений Алексеевич",
                Inn = "999999999998",
                Salary = 87000m,
                PersonalCode = "90-09908"
            },
            new Employee
            {
                Department = "ФинОтдел",
                Name = "Андреев Сергей Леонидович",
                Inn = "999999999997",
                Salary = 47000m,
                PersonalCode = "50-11178"
            }
        };

        private readonly string[] Months =
            { "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь" };

        [OneTimeSetUp]
        public void SetUp()
        {
            var employeeServiceMock = new Mock<IEmployeeService>();
            employeeServiceMock.Setup(x => x.GetEmployees()).ReturnsAsync(employees);

            _reportManager = new ReportManager(employeeServiceMock.Object);
        }

        [TestCase(2020, 3)]
        public async Task ReportStrings_Test(int year, int month)
        {
            var reportString = await _reportManager.BuildReport(year, month);

            var depEmployees = employees.Where(x => string.Equals(x.Department, "ИТ")).ToList();
            var depTotal = depEmployees.Sum(x => x.Salary);
            Assert.IsTrue(reportString.Contains(depTotal.ToString("#.##")));

            foreach (var employee in employees)
            {
                Assert.IsTrue(reportString.Contains(employee.Name, StringComparison.InvariantCultureIgnoreCase));
            }

            var total = employees.Sum(x => x.Salary);
            Assert.IsTrue(reportString.Contains(total.ToString("#.##")));
        }

        [TestCase(2020, 1)]
        [TestCase(2019, 2)]
        [TestCase(2018, 3)]
        [TestCase(2017, 4)]
        [TestCase(2016, 5)]
        [TestCase(2015, 6)]
        [TestCase(2014, 7)]
        [TestCase(2013, 8)]
        [TestCase(2012, 9)]
        [TestCase(2011, 10)]
        [TestCase(2010, 11)]
        [TestCase(2009, 12)]
        public async Task ReportMonth_Test(int year, int month)
        {
            var reportString = await _reportManager.BuildReport(year, month);
            Assert.IsTrue(reportString.Contains(Months[month - 1]));
            Assert.IsTrue(reportString.Contains(year.ToString()));
        }
    }
}