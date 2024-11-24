using Api.BusinessLogic.Calculations.Interfaces;
using Api.BusinessLogic.Models;

namespace Api.BusinessLogic.Calculations.Deductions
{
    public class SeniorBenefitsDeductionCalculator : BaseDeductionCalculator, IDeduction
    {
        public SeniorBenefitsDeductionCalculator(ICalculationsLibrary library) : base(library) { }
        public async Task<PayStatement> CalculateDeduction(PayStatement payStatement)
        {
            var deduction = 0M;
            var hasDeduction = payStatement.Employee.DateOfBirth <= DateTime.UtcNow.AddYears(Constants.SENIOR_BENFITS_AGE_FLOOR * -1);

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
