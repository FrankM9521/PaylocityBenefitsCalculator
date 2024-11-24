using Api.BusinessLogic.Models.Request;
using Api.BusinessLogic.Models.Response;

namespace Api.BusinessLogic.Services
{
    public interface ICalculatePayCheckService
    {
        Task<CalculateCheckResponse> Calculate(CalculatePayrollRequest request);
    }
}
