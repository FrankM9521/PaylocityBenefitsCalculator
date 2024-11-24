using Api.BusinessLogic.Models;

namespace Api.BusinessLogic.Calculations.Deductions
{
    public interface IDeduction
    {
        Task<PayStatement> CalculateDeduction(PayStatement payStatement);
    }
}
