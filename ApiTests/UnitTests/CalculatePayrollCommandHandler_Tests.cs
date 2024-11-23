using Api.Calculations;
using Api.Commands;
using Api.Enitiies;
using Api.Mappers;
using Api.Repositories;
using Api.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using static ApiTests.UnitTests.PayStatementCalculations_Tests;
using Api.Models;

namespace ApiTests.UnitTests
{
    public class CalculatePayrollCommandHandler_Tests
    {
        private CalculatePayrollCommandHandler _sut;
        private Mock<IEmployeeService> _employeeServiceMock = new Mock<IEmployeeService>();

        public CalculatePayrollCommandHandler_Tests()
        {
            _sut = new CalculatePayrollCommandHandler(_employeeServiceMock.Object, new DeductionCalculatorCollection());

        }

        [Theory]
        // Standard User, No Dependents
        //    [InlineData(52000, 0, 461.54, 1538.46, Api.Models.DeductionTypes.BenefitsBase, 461.54)]
        //   [InlineData(12000, 0, 461.54, 0.0, Api.Models.DeductionTypes.BenefitsBase, 461.54)]
        //[InlineData(6000, 0, 230.77, 0.0, Api.Models.DeductionTypes.BenefitsBase, 230.77)]
        // Standard User with dependents
        /* [InlineData(52000, 1, 738.46, 1261.54, Api.Models.DeductionTypes.DependentBenefitsFee, 276.92)]
         [InlineData(52000, 2, 1015.39, 984.61, Api.Models.DeductionTypes.DependentBenefitsFee, 553.85)]
          [InlineData(52000, 10, 2000, 0, Api.Models.DeductionTypes.DependentBenefitsFee, 1538.46)]
      */
        //High Earner With and without Dependents 
        //  [InlineData(120000, 0, 553.85, 4061.53, DeductionTypes.HighEarnerBenefitsFee, 92.31)]
        //  [InlineData(120000, 2,  1107.70, 3507.68, DeductionTypes.HighEarnerBenefitsFee, 92.31)]

        // Senior With and without Dependents 
        //[InlineData(52000, 0, 553.85, 1446.15, Api.Models.DeductionTypes.SeniorBenefitsFee, 92.31, 51)]
        //[InlineData(52000, 2, 1107.70, 892.30, Api.Models.DeductionTypes.SeniorBenefitsFee, 92.31, 51)]

        // Senior is OVER 50
        //[InlineData(52000, 0, 461.54, 1538.46, Api.Models.DeductionTypes.SeniorBenefitsFee, 0, 50)]

        //Senior High Earner With and without Dependents 
          [InlineData(120000, 0, 553.85, 4061.53, Api.Models.DeductionTypes.HighEarnerBenefitsFee, 92.31, 51)]
          [InlineData(120000, 2,  1107.70, 3507.68, Api.Models.DeductionTypes.HighEarnerBenefitsFee, 92.31, 51)]

        public async Task ItCalculatesCorrectly(decimal salary, int numberOfDependents, decimal totalDeductionAmt, decimal netPay, Api.Models.DeductionTypes deductionTypeToVerify, decimal deductionAmountToVerify, int age = 30)
        {
            //Arrange
            var request = new CalculatePayrollCommand   { EmployeeID = 1 };
            var employee = GetEmployee(request.EmployeeID, salary, numberOfDependents, age);

            _employeeServiceMock.Setup(s => s.GetByID(It.IsAny<int>())).ReturnsAsync(employee);

            var result = await _sut.Handle(request, default(CancellationToken));

            Assert.NotNull(result);

            var deductionAmount = result.PayStatement.Deductions[deductionTypeToVerify];

            Assert.Equal(deductionAmount, deductionAmountToVerify);
            Assert.Equal(totalDeductionAmt, result.PayStatement.TotalDeductions);
            Assert.Equal(netPay, result.PayStatement.NetPay);
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
