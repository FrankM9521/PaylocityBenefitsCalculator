using Paylocity.Employees.Models;

namespace Paylocity.PayChecks.Models.Request
{
    public record CalculatePayrollRequest(Employee Employee, IEnumerable<PayCheck>? PreviousPayStatements = null);
}
