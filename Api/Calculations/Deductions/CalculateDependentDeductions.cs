using Api.Models;

namespace Api.Calculations.Deductions
{
    public class CalculateDependentDeductions : IDeduction
    {
        public async Task<PayStatement> CalculateDeduction(PayStatement payStatement)
        {
            await payStatement.AddDeduction(DeductionTypes.DependentBenefitsFee, Calculations.GetDependentDeduction(payStatement));

            return payStatement;
        }
    }
}
