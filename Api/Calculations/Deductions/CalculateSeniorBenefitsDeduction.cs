using Api.Models;

namespace Api.Calculations.Deductions
{
    public class CalculateSeniorBenefitsDeduction : IDeduction
    {
        public async Task<PayStatement> CalculateDeduction(PayStatement payStatement)
        {
            var deduction = 0M;
            var hasDeduction = payStatement.Employee.DateOfBirth <= DateTime.UtcNow.AddYears(Constants.SENIOR_BENFITS_AGE_FLOOR * -1);

            if (hasDeduction)
            {
                deduction = Calculations.GetSeniorDeduction(payStatement);

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
