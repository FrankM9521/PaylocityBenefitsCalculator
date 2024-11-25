using Api.BusinessLogic.Models.Request;
using Api.BusinessLogic.Models.Response;

namespace Api.BusinessLogic.Services.Interfaces
{
    public interface ICalculatePayCheckService
    {
        Task<CalculateCheckResponse> Calculate(CalculatePayrollRequest request);
    }
}
