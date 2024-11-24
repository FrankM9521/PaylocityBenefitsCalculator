using Api.BusinessLogic.Models.CalculatePayroll;

namespace Api.BusinessLogic.Models.Request
{
    public record CalculatePayrollRequest(Employee Employee, IEnumerable<PayCheck>? PreviousPayStatements = null);
}
