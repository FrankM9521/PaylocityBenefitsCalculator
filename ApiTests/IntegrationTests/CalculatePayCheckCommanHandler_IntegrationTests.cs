using Api.BusinessLogic.Commands;
using Api.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using static ApiTests.UnitTests.PayStatementCalculations_Tests;
using Api.BusinessLogic.Services;
using Api.Api.Utility;
using Api.Data.Repositories;
using Api.BusinessLogic.Mappers;
using Api.BusinessLogic.Factories;
using Api.Entities;
using Api.Repositories.Interfaces;
using Paylocity.Employees.Api.BusinessLogic.Models;
using Paylocity.Employees.Api.BusinessLogic.Services.Interfaces;
using Paylocity.Employees.Api.BusinessLogic.Validation;

namespace ApiTests.IntegrationTests
{
    [Collection("CalculatePayCheckCommanHandler")]
    public class CalculatePayCheckCommanHandler_IntegrationTests
    {
        private CalculatePayCheckCommandHandler _sut;
        private IEmployeeService _employeeService;
        private IValidationCollection<Employee> _employeeValidationCollection;
        private IPayCheckRepository _payCheckRepository;
        private ICalculationLibraryFactory _calculationLibraryFactory;
        private IPayCheckService _payCheckService;
        private IDbContextAccessor _dbContextAccessor;
        private IEmployeeRepository _employeeRepository;
        private ICalculatePayCheckService _calculatePayCheckService;

        public CalculatePayCheckCommanHandler_IntegrationTests()
        {
            _dbContextAccessor = new DbContextAccessor();
            var dependentRepository = new DependentRepository(new DependentValidationCollection(_dbContextAccessor), _dbContextAccessor);
            _employeeValidationCollection = new EmployeeValidationCollection(_dbContextAccessor, new BenefitsConfig());
            _employeeRepository = new EmployeeRepository(_dbContextAccessor);
            _employeeService = new EmployeeService(_employeeRepository, dependentRepository);
            _payCheckRepository = new PayCheckRepository(_dbContextAccessor, dependentRepository);
            _payCheckService = new PayCheckService(_payCheckRepository);
            _calculatePayCheckService = new CalculatePayCheckService(_payCheckRepository, new BenefitsConfig(), new CalculationLibraryFactory(new BenefitsConfig()));
            _sut = new CalculatePayCheckCommandHandler(_employeeService, _employeeValidationCollection, _calculatePayCheckService, _payCheckService);
        }

        [Theory]
        [InlineData(49377.75, 0, 30, 12000)]
        public async Task ItCalculatesYearCorrectly(decimal salary, int numberOfDependents, int age, decimal baseBenefits, decimal dependentBenefits = 0, decimal highEarnerBenefits = 0, decimal seniorBenefits = 0)
        {
            var request = new CalculatePayCheckCommand(1);
            var employee = GetEmployee(request.EmployeeID, salary, numberOfDependents, age);

            CalculatePayCheckCommandResponse response = null;
            for (int i = 0; i < 26; i++)
            {
                response = await _sut.Handle(request, default);
            }

            var payChecks = await _payCheckService.GetByEmployeeID(employee.Id);

            var gross = payChecks.Sum(pay => pay.GrossPay);
            var baseBenefitsTest = payChecks.Sum(pay => pay.Deductions[DeductionTypes.BenefitsBase]);
            var depBenefits = payChecks.Sum(pay => pay.Deductions[DeductionTypes.DependentBenefitsFee]);
            var heBenefits = payChecks.Sum(pay => pay.Deductions[DeductionTypes.HighEarnerBenefitsFee]);
            var senBenefits = payChecks.Sum(pay => pay.Deductions[DeductionTypes.SeniorBenefitsFee]);

            Assert.Equal(26, payChecks.Count());
            Assert.Equal(employee.Salary, payChecks.Sum(pay => pay.GrossPay));
            Assert.Equal(baseBenefits, payChecks.Sum(pay => pay.Deductions[DeductionTypes.BenefitsBase]));
            Assert.Equal(dependentBenefits, payChecks.Sum(pay => pay.Deductions[DeductionTypes.DependentBenefitsFee]));
            Assert.Equal(highEarnerBenefits, payChecks.Sum(pay => pay.Deductions[DeductionTypes.HighEarnerBenefitsFee]));
            Assert.Equal(seniorBenefits, payChecks.Sum(pay => pay.Deductions[DeductionTypes.SeniorBenefitsFee]));
        }

        [Fact]
        public async Task When26ChecksExist_ItDoesNotAddAnother()
        {
            _dbContextAccessor.ClearPaychecks();
            var request = new CalculatePayCheckCommand(1);
            var employee = GetEmployee(request.EmployeeID, 60000, 0);
            var checks = new List<CalculatePayCheckCommandResponse>();

            for (int i = 0; i < 26; i++)
            {
                checks.Add(await _sut.Handle(request, default));
            }

            var checkResult = await _sut.Handle(request, default);

            Assert.True(checks.All(c => c.Success));
            Assert.False(checkResult.Success);
        }

        private static Employee GetEmployee(int id, decimal salary, int numberOfDependents, int age = 30)
        {
            var employee = TestHelpers.TestEmployee(id, salary, numberOfDependents, age);

            var empList = new List<EmployeeEntity> { employee.ToEntity() };

            var depList = employee.Dependents.Select(d => d.ToEntity()).ToList();

            DB.SetData(empList, depList);

            return employee;
        }

        private static DataBase DB
        {
            get
            {
                return DataBase.Instance;
            }
        }
    }
}
