using Api.BusinessLogic.Models.CalculatePayroll;
using Api.BusinessLogic.Validation;

namespace Api.BusinessLogic.Commands
{
    public record CalculatePayCheckCommandResponse(CalculatePayrollEmployee Employee, CalculatePayrollStatement PayCheck, bool Success = true, string ErrorMessage = "") : ValidationResponse(Success, ErrorMessage);
}
