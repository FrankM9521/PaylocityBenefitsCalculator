using Api.BusinessLogic.Calculations.Interfaces;

namespace Api.BusinessLogic.Calculations.Deductions
{
    public abstract class BaseDeductionCalculator
    {
        protected readonly ICalculationsLibrary _calculationsLibrary;
        protected BaseDeductionCalculator(ICalculationsLibrary calculationsLibrary)
        {
            _calculationsLibrary = calculationsLibrary;
        }
    }
}
