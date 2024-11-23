using Api.Models;

namespace Api.Calculations.Deductions
{
    public class CalculateHighEarnerDeduction : IDeduction
    {
        public async Task<PayStatement> CalculateDeduction(PayStatement payStatement)
        {
            await payStatement.AddDeduction(DeductionTypes.HighEarnerBenefitsFee, Calculations.GetHighEarnersDeduction(payStatement));

            return payStatement;
        }
    }
}
