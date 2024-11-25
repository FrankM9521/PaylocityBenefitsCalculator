using Api.Api.Utility;
using Api.BusinessLogic.Calculations;
using Api.BusinessLogic.Calculations.Interfaces;
using Api.BusinessLogic.Mappers;
using Api.BusinessLogic.Models;
using Api.BusinessLogic.Models.CalculatePayroll;
using Api.BusinessLogic.Models.Request;
using Api.BusinessLogic.Models.Response;
using Api.BusinessLogic.Services.Interfaces;
using Api.Data.Repositories.Interfaces;

namespace Api.BusinessLogic.Services
{
    /// <summary>
    /// Does the actual Pay Check Calculations 
    /// </summary>
    public class CalculatePayCheckService : ICalculatePayCheckService
    {
        private readonly IPayCheckRepository _payCheckRepository;
        private readonly IBenefitsConfig _benefitsConfig;
        private readonly ICalculationLibraryFactory _calculationLibraryFactory;
        public CalculatePayCheckService(IPayCheckRepository payCheckRepository, IBenefitsConfig benefitsConfig, ICalculationLibraryFactory calculationLibraryFactory)
        {
            _payCheckRepository = payCheckRepository;
            _benefitsConfig = benefitsConfig;
            _calculationLibraryFactory = calculationLibraryFactory;
        }
        public async Task<CalculateCheckResponse> Calculate(CalculatePayrollRequest request)
        {
            var deductionCalculators = GetDeductionCalculators(request.PreviousPayStatements?.Count() > 0 ? request.PreviousPayStatements.Count() + 1 : 1);
           
            // Start with our Gross Pay
            var payStatement = new CalculatePayCheck(request.PreviousPayStatements, request.Employee)
            {
                GrossPay = GetGrossPay(request.Employee.Salary, request.PreviousPayStatements)
            };

            // Run through our deductions
            foreach (var calculator in deductionCalculators.Deductions)
            {
                payStatement = await calculator.Calculate(payStatement);
            }

            // add to DB
            await _payCheckRepository.Create(payStatement.ToModel());

            return new CalculateCheckResponse(
                    new CalculatePayrollEmployee(request.Employee.FirstName, request.Employee.LastName, request.Employee.Salary, request.Employee.DateOfBirth),
                    new CalculatePayrollStatement(payStatement.ID, payStatement.Order, payStatement.GrossPay, payStatement.NetPay, payStatement.Deductions.Count(), payStatement.Deductions));
        }

        /// <summary>
        /// On the final pay period, take the amount paid so far this year and subtract from
        /// the salary to pick up any lost amounts from rounding
        /// </summary>
        /// <param name="salary"></param>
        /// <param name="previousPayChecks"></param>
        /// <returns></returns>
        private decimal GetGrossPay(decimal salary, IEnumerable<PayCheck>? previousPayChecks)
        {
            var payPeriod = ( previousPayChecks?.Count() ?? 0)  + 1;

            return payPeriod == _benefitsConfig.PAY_PERIODS_PER_YEAR
                ? salary - previousPayChecks?.Sum(pay => pay.GrossPay) ?? 0
                : Math.Round(salary / _benefitsConfig.PAY_PERIODS_PER_YEAR, 2);
        }

        /// <summary>
        /// Get the correct claculator for the pay period. Either standard or Lastt Pay Check of Year
        /// </summary>
        /// <param name="payCheckPeriod"></param>
        /// <returns></returns>
        private ICalculatorCollection GetDeductionCalculators(int payCheckPeriod)
        {
            var calculatorLibraryType = _calculationLibraryFactory.Create(payCheckPeriod);

            return new DeductionCalculatorCollection(calculatorLibraryType, _benefitsConfig);
        }
    }
}
