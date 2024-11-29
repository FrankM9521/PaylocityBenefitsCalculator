using Paylocity.PayChecks.Models.Request;
using Paylocity.PayChecks.Models.Response;

namespace Paylocity.PayChecks.Services.Interfaces
{
    public interface ICalculatePayCheckService
    {
        Task<CalculateCheckResponse> Calculate(CalculatePayrollRequest request);
    }
}
