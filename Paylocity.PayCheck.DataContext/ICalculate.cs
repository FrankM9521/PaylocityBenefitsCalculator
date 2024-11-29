using Paylocity.PayChecks.Models;

namespace Paylocity.PayChecks.Services.Interfaces
{
    public interface ICalculate
    {
        Task<CalculatePayCheck> Calculate(CalculatePayCheck payStatement);
    }
}
