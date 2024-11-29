using Paylocity.PayChecks.Models;
using Paylocity.PayChecks.Services.Interfaces;

namespace Paylocity.PayChecks.Services.Calculators.Deductions
{
    public class DependentDeductionCalculator : BaseDeductionCalculator, ICalculate
    {
        public DependentDeductionCalculator(ICalculationsLibrary library) : base(library) { }
        public async Task<CalculatePayCheck> Calculate(CalculatePayCheck payStatement)
        {
            await payStatement.AddDeduction(DeductionTypes.DependentBenefitsFee, _calculationsLibrary.GetDependentDeduction(payStatement));

            return payStatement;
        }
    }
}
