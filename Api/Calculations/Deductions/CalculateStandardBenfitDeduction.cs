using Api.Models;

namespace Api.Calculations.Deductions
{
    public class CalculateStandardBenfitDeduction : IDeduction
    {
        public Task<PayStatement> CalculateDeduction(PayStatement payStatement)
        {
            var baseDeduction = Calculations.GetBaseDeduction(payStatement);

            if (baseDeduction > payStatement.NetPay)
            {
                baseDeduction = payStatement.NetPay;
            }

            payStatement.AddDeduction(DeductionTypes.BenefitsBase, baseDeduction);

            return Task.FromResult(payStatement);
        }
    }
}
