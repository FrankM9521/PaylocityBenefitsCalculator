using Api.Api.Utility;
using Api.BusinessLogic.Calculations;
using Api.BusinessLogic.Calculations.Interfaces;
using Api.BusinessLogic.Models;
using Api.BusinessLogic.Models.CalculatePayroll;
using Api.BusinessLogic.Models.Request;
using Api.BusinessLogic.Models.Response;
using Api.Data.Repositories;

namespace Api.BusinessLogic.Services
{
    public interface ICalculatePayrollService
    {
        Task<CalculatePayrollResponse> Calculate(CalculatePayrollRequest request);
    }
    public class CalculatePayrollService : ICalculatePayrollService
    {
        private IPayStatementRepository _paymentRepository;
        private readonly IBenefitsConfig _benefitsConfig;
        public CalculatePayrollService(IPayStatementRepository payrollRepository, IBenefitsConfig benefitsConfig)
        {
            _paymentRepository = payrollRepository;
            _benefitsConfig = benefitsConfig;
        }
        public async Task<CalculatePayrollResponse> Calculate(CalculatePayrollRequest request)
        {
            var deductionCalculators = GetDeductionCalculators(request.PreviousPayStatements?.Count() > 0 ? request.PreviousPayStatements.Count() + 1 : 1);
           
            var payStatement = new CalculatePayStatement(request.PreviousPayStatements, request.Employee)
            {
                GrossPay = Math.Round(request.Employee.Salary / _benefitsConfig.PAY_PERIODS_PER_YEAR, 2)
            };

            foreach (var calculator in deductionCalculators.Deductions)
            {
                payStatement = await calculator.Calculate(payStatement);
            }

            await _paymentRepository.Create(payStatement);

            return new CalculatePayrollResponse(
                    new CalculatePayrollEmployee(request.Employee.FirstName, request.Employee.LastName, request.Employee.Salary, request.Employee.DateOfBirth),
                    new CalculatePayrollStatement(payStatement.ID, payStatement.Order, payStatement.GrossPay, payStatement.NetPay, payStatement.Deductions.Count(), payStatement.Deductions));
        }
        private IDeductionCalculatorCollection GetDeductionCalculators(int payCheckPeriod)
        {
            // if period == 26, use different calculators
            var calculators = new StandardCalculationLibrary(_benefitsConfig);

            return new DeductionCalculatorCollection(calculators, _benefitsConfig);
        }
    }
}
