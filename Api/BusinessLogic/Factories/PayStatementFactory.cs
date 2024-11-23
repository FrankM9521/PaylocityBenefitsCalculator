using Api.BusinessLogic.Calculations;
using Api.BusinessLogic.Calculations.Interfaces;
using Api.BusinessLogic.Mappers;
using Api.BusinessLogic.Models;
using Api.BusinessLogic.Models.CalculatePayroll;
using Api.Data.Repositories;

namespace Api.BusinessLogic.Factories
{
    public class PayStatementFactory : IFactory<CreatePayStatmentResponse, CreatePayStatementRequest>
    {
        public async Task<CreatePayStatmentResponse> Create(CreatePayStatementRequest request)
        {
            var deductionCalculators = GetDeductionCalculators(request.PreviousPayStatements?.Count() > 0 ? request.PreviousPayStatements.Count() + 1 : 1);
            var payStatement = new PayStatement
            {
                Employee = request.Employee,
                GrossPay = Math.Round(request.Employee.Salary / Constants.PAY_PERIODS_PER_YEAR, 2)
            };

            foreach (var calculator in deductionCalculators.Deductions)
            {
                payStatement = await calculator.CalculateDeduction(payStatement);
            }

            DB.Add(payStatement.ToEntity());

            return new CreatePayStatmentResponse(
                    new CalculatePayrollEmployee(request.Employee.FirstName, request.Employee.LastName, request.Employee.Salary, request.Employee.DateOfBirth),
                    new CalculatePayrollPayStatement(payStatement.ID, payStatement.Order, payStatement.GrossPay, payStatement.NetPay, payStatement.Deductions.Count(), payStatement.Deductions));
        }

        private DataBase DB
        {
            get { return DataBase.Instance; }
        }

        private IDeductionCalculatorCollection GetDeductionCalculators(int payCheckPeriod)
        {
            // if period == 26, use different calculators
            var calculators = new StandardCalculationLibrary();

            return new DeductionCalculatorCollection(calculators);
        }

    }
}
