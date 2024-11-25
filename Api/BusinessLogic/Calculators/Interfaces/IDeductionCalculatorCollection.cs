using Api.BusinessLogic.Calculators;

namespace Api.BusinessLogic.Calculations.Interfaces
{
    public interface ICalculatorCollection
    {
        IEnumerable<ICalculate> Deductions { get; }
    }
}
