namespace Paylocity.PayChecks.Services.Interfaces
{
    public interface IDeductionCalculatorCollection : ICalculatorCollection
    {

    }
    public interface ICalculatorCollection
    {
        IEnumerable<ICalculate> Deductions { get; }
    }
}
