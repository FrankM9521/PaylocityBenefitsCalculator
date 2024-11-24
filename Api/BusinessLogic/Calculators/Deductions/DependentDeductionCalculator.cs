using Api.BusinessLogic.Calculations.Interfaces;
using Api.BusinessLogic.Models;

namespace Api.BusinessLogic.Calculations.Deductions
{
    public class DependentDeductionCalculator : BaseDeductionCalculator, IDeduction
    {
        public DependentDeductionCalculator(ICalculationsLibrary library) : base(library) { }
        public async Task<PayStatement> CalculateDeduction(PayStatement payStatement)
        {
            await payStatement.AddDeduction(DeductionTypes.DependentBenefitsFee, _calculationsLibrary.GetDependentDeduction(payStatement));

            return payStatement;
        }
    }
}
