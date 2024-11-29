using Paylocity.PayChecks.Models.CalculatePayroll;
using Paylocity.Validation.Models;

namespace Paylocity.PayChecks.Commands
{
    public record CalculatePayCheckCommandResponse(CalculatePayrollEmployee Employee, CalculatePayrollStatement PayCheck, bool Success = true, string ErrorMessage = "") : ValidationResponse(Success, ErrorMessage);
}
