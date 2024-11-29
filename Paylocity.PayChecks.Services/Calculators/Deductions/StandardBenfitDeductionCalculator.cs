using Paylocity.PayChecks.Models;
using Paylocity.PayChecks.Services.Interfaces;

namespace Paylocity.PayChecks.Services.Calculators.Deductions
{
    public class StandardBenfitDeductionCalculator : BaseDeductionCalculator, ICalculate
    {
        public StandardBenfitDeductionCalculator(ICalculationsLibrary library) : base(library) { }
        public async Task<CalculatePayCheck> Calculate(CalculatePayCheck payStatement)
        {
            var baseDeduction = _calculationsLibrary.GetBaseDeduction(payStatement);

            if (baseDeduction > payStatement.NetPay)
            {
                baseDeduction = payStatement.NetPay;
            }

            await payStatement.AddDeduction(DeductionTypes.BenefitsBase, baseDeduction);

            return payStatement;
        }
    }
}
