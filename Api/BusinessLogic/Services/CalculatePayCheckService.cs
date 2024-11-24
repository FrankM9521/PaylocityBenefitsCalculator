using Api.Api.Utility;
using Api.BusinessLogic.Calculations;
using Api.BusinessLogic.Calculations.Interfaces;
using Api.BusinessLogic.Mappers;
using Api.BusinessLogic.Models;
using Api.BusinessLogic.Models.CalculatePayroll;
using Api.BusinessLogic.Models.Request;
using Api.BusinessLogic.Models.Response;
using Api.Data.Repositories.Interfaces;

namespace Api.BusinessLogic.Services
{
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
           
            var payStatement = new CalculatePayCheck(request.PreviousPayStatements, request.Employee)
            {
                GrossPay = Math.Round(request.Employee.Salary / _benefitsConfig.PAY_PERIODS_PER_YEAR, 2)
            };

            foreach (var calculator in deductionCalculators.Deductions)
            {
                payStatement = await calculator.Calculate(payStatement);
            }

            await _payCheckRepository.Create(payStatement.ToModel());

            return new CalculateCheckResponse(
                    new CalculatePayrollEmployee(request.Employee.FirstName, request.Employee.LastName, request.Employee.Salary, request.Employee.DateOfBirth),
                    new CalculatePayrollStatement(payStatement.ID, payStatement.Order, payStatement.GrossPay, payStatement.NetPay, payStatement.Deductions.Count(), payStatement.Deductions));
        }
        private IDeductionCalculatorCollection GetDeductionCalculators(int payCheckPeriod)
        {
            var calculatorLibraryType = _calculationLibraryFactory.Create(payCheckPeriod);

            return new DeductionCalculatorCollection(calculatorLibraryType, _benefitsConfig);
        }
    }


    public interface ICalculationLibraryFactory
    {
        public ICalculationsLibrary Create(int payCheckPeriod);
    }
    public class CalculationLibraryFactory : ICalculationLibraryFactory
    {
        private readonly IBenefitsConfig _benefitsConfig;
        public CalculationLibraryFactory(IBenefitsConfig benefitsConfig)
        {
            _benefitsConfig = benefitsConfig;
        }
        public ICalculationsLibrary Create(int payCheckPeriod)
        {
            switch(payCheckPeriod)
            {
                case var exp when payCheckPeriod < _benefitsConfig.PAY_PERIODS_PER_YEAR:
                    return new StandardCalculationLibrary(_benefitsConfig);
                case var exp when payCheckPeriod == _benefitsConfig.PAY_PERIODS_PER_YEAR:
                    return new LastPayCheckOfYearCalculator(_benefitsConfig);
                default:
                    throw new NotImplementedException("Calculator Not Implemented");
            }
        }
    }
}
