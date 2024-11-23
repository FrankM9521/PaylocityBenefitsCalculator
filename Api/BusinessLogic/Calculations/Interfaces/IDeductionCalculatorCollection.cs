using Api.BusinessLogic.Calculations.Deductions;

namespace Api.BusinessLogic.Calculations.Interfaces
{
    public interface IDeductionCalculatorCollection
    {
        IEnumerable<IDeduction> Deductions { get; }
    }
}
