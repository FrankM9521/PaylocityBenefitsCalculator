using Api.BusinessLogic.Calculations.Interfaces;

namespace Api.BusinessLogic.Calculations.Deductions
{
    public abstract class CalculateDeductionBase
    {
        protected readonly ICalculationsLibrary _calculationsLibrary;
        protected CalculateDeductionBase(ICalculationsLibrary calculationsLibrary)
        {
            _calculationsLibrary = calculationsLibrary;
        }
    }
}
