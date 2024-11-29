using Paylocity.PayChecks.Services.Interfaces;

namespace Paylocity.PayChecks.Services.Calculators.Deductions
{
    public class DeductionCalculatorCollection : IDeductionCalculatorCollection
    {
        public IEnumerable<ICalculate> Deductions => throw new NotImplementedException();
    }
}
