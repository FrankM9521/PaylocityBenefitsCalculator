using Paylocity.PayChecks.Models;
using Paylocity.PayChecks.Services.Interfaces;
using Paylocity.Shared.Config;

namespace Paylocity.PayChecks.Services.Calculators.Deductions
{
    public class SeniorBenefitsDeductionCalculator : BaseDeductionCalculator, ICalculate
    {
        private readonly IBenefitsConfig _benefitsConfig;
        public SeniorBenefitsDeductionCalculator(ICalculationsLibrary library, IBenefitsConfig benefitsConfig) : base(library)
        {
            _benefitsConfig = benefitsConfig;
        }
        public async Task<CalculatePayCheck> Calculate(CalculatePayCheck payStatement)
        {
            var deduction = 0M;
            var hasDeduction = payStatement.EmployeeDateOfBirth <= DateTime.UtcNow.AddYears(_benefitsConfig.SENIOR_BENFITS_AGE_FLOOR * -1);

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
