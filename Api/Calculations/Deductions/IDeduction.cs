using Api.Models;

namespace Api.Calculations.Deductions
{
    public interface IDeduction
    {
        Task<PayStatement> CalculateDeduction(PayStatement payStatement);
    }
}
