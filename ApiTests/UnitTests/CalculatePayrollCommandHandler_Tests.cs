using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using static ApiTests.UnitTests.PayStatementCalculations_Tests;
using Api.BusinessLogic.Commands;
using Api.Data.Entities;
using Api.BusinessLogic.Mappers;
using Api.BusinessLogic.Models;
using Api.BusinessLogic.Services;
using Api.BusinessLogic.Validation;
using Api.Data;

namespace ApiTests.UnitTests
{
    public class CalculatePayrollCommandHandler_Tests
    {
        private CalculatePayrollCommandHandler _sut;
        private Mock<IEmployeeService> _employeeServiceMock = new Mock<IEmployeeService>();
        private Mock<IValidationCollection<Employee>> _employeeValidationMock = new Mock<IValidationCollection<Employee>>();

        public CalculatePayrollCommandHandler_Tests()
        {
            _employeeValidationMock.Setup(x => x.Validate(It.IsAny<Employee>())).ReturnsAsync(new ValidationResponse());

       
           _sut = new CalculatePayrollCommandHandler(_employeeServiceMock.Object, _employeeValidationMock.Object, new CalculatePayrollService());

        }

        [Theory]
        // Standard User, No Dependents
        //    [InlineData(52000, 0, 461.54, 1538.46, Api.Models.DeductionTypes.BenefitsBase, 461.54)]
      //     [InlineData(12000, 0, 461.54, 0.0, Api.Models.DeductionTypes.BenefitsBase, 461.54)]
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
          [InlineData(120000, 0, 646.16, 3969.22, Api.BusinessLogic.Models.DeductionTypes.HighEarnerBenefitsFee, 92.31, 51)]
          [InlineData(120000, 2,  1200.01, 3415.37, Api.BusinessLogic.Models.DeductionTypes.HighEarnerBenefitsFee, 92.31, 51)]

        public async Task ItCalculatesCorrectly(decimal salary, int numberOfDependents, decimal totalDeductionAmt, decimal netPay, Api.BusinessLogic.Models.DeductionTypes deductionTypeToVerify, decimal deductionAmountToVerify, int age = 30)
        {
            //Arrange
            var request = new CalculatePayrollCommand(1);
            var employee = GetEmployee(request.EmployeeID, salary, numberOfDependents, age);

            _employeeServiceMock.Setup(s => s.GetByID(It.IsAny<int>())).ReturnsAsync(employee);

            var result = await _sut.Handle(request, default(CancellationToken));

            Assert.NotNull(result);

            var deductionAmount = result.PayCheck.Deductions[deductionTypeToVerify];
            var deduct = result.PayCheck.Deductions.Sum(d => d.Value);

            Assert.Equal(deductionAmount, deductionAmountToVerify);
            Assert.Equal(totalDeductionAmt, result.PayCheck.Deductions.Sum(d => d.Value));
            Assert.Equal(netPay, result.PayCheck.NetPay);
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
