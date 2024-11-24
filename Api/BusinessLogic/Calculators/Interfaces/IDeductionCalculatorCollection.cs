using Api.BusinessLogic.Calculators;

namespace Api.BusinessLogic.Calculations.Interfaces
{
    public interface IDeductionCalculatorCollection
    {
        IEnumerable<ICalculate> Deductions { get; }
    }
}
