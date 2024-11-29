using Paylocity.PayChecks.Models;
using Paylocity.PayChecks.Services.Interfaces;

namespace Paylocity.PayChecks.Services.Calculators.Deductions
{
    public class CalculateHighEarnerDeductionCalculator : BaseDeductionCalculator, ICalculate
    {
        public CalculateHighEarnerDeductionCalculator(ICalculationsLibrary calculationsLibrary) : base(calculationsLibrary) { }

        public async Task<CalculatePayCheck> Calculate(CalculatePayCheck payStatement)
        {
            await payStatement.AddDeduction(DeductionTypes.HighEarnerBenefitsFee, _calculationsLibrary.GetHighEarnersDeduction(payStatement));

            return payStatement;
        }
    }
}
