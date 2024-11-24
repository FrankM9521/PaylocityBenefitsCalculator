using Api.BusinessLogic.Calculations.Interfaces;
using Api.BusinessLogic.Calculators;
using Api.BusinessLogic.Models;

namespace Api.BusinessLogic.Calculations.Deductions
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
