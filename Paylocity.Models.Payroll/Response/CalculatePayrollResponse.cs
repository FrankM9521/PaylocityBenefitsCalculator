using Paylocity.PayChecks.Models.CalculatePayroll;

namespace Paylocity.PayChecks.Models.Response
{
    public record CalculateCheckResponse(CalculatePayrollEmployee Employee, CalculatePayrollStatement PayCheck);
}
