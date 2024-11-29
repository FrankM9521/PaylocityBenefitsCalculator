using Paylocity.PayChecks.Services.Interfaces;

namespace Paylocity.PayChecks.Services.Calculators.Deductions
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
