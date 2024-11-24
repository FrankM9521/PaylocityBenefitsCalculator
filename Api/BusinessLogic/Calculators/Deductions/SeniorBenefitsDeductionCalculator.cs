using Api.Api.Utility;
using Api.BusinessLogic.Calculations.Interfaces;
using Api.BusinessLogic.Calculators;
using Api.BusinessLogic.Models;

namespace Api.BusinessLogic.Calculations.Deductions
{
    public class SeniorBenefitsDeductionCalculator : BaseDeductionCalculator, ICalculate
    {
        private readonly IBenefitsConfig _benefitsConfig;
        public SeniorBenefitsDeductionCalculator(ICalculationsLibrary library, IBenefitsConfig benefitsConfig) : base(library) 
        {
            _benefitsConfig = benefitsConfig;   
        }
        public async Task<PayStatement> Calculate(PayStatement payStatement)
        {
            var deduction = 0M;
            var hasDeduction = payStatement.Employee.DateOfBirth <= DateTime.UtcNow.AddYears(_benefitsConfig.SENIOR_BENFITS_AGE_FLOOR * -1);

            if (hasDeduction)
            {
                deduction = _calculationsLibrary.GetSeniorDeduction(payStatement);

                if (deduction > payStatement.NetPay)
                {
                    deduction = payStatement.NetPay;
                }
            }

            await payStatement.AddDeduction(DeductionTypes.SeniorBenefitsFee, deduction);

            return payStatement;
        }
    }
}
