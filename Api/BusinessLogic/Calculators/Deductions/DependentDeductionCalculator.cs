using Api.BusinessLogic.Calculations.Interfaces;
using Api.BusinessLogic.Calculators;
using Api.BusinessLogic.Models;

namespace Api.BusinessLogic.Calculations.Deductions
{
    public class DependentDeductionCalculator : BaseDeductionCalculator, ICalculate
    {
        public DependentDeductionCalculator(ICalculationsLibrary library) : base(library) { }
        public async Task<PayStatement> Calculate(PayStatement payStatement)
        {
            await payStatement.AddDeduction(DeductionTypes.DependentBenefitsFee, _calculationsLibrary.GetDependentDeduction(payStatement));

            return payStatement;
        }
    }
}
