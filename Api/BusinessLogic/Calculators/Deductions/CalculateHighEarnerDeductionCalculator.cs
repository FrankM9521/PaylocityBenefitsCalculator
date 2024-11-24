using Api.BusinessLogic.Calculations.Interfaces;
using Api.BusinessLogic.Models;

namespace Api.BusinessLogic.Calculations.Deductions
{
    public class CalculateHighEarnerDeductionCalculator : BaseDeductionCalculator, IDeduction
    {
        public CalculateHighEarnerDeductionCalculator(ICalculationsLibrary calculationsLibrary) : base(calculationsLibrary) { }

        public async Task<PayStatement> CalculateDeduction(PayStatement payStatement)
        {
            await payStatement.AddDeduction(DeductionTypes.HighEarnerBenefitsFee, _calculationsLibrary.GetHighEarnersDeduction(payStatement));

            return payStatement;
        }
    }
}
