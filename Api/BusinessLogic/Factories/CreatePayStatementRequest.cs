using Api.BusinessLogic.Models;
using Api.BusinessLogic.Models.CalculatePayroll;

namespace Api.BusinessLogic.Factories
{
    public record CreatePayStatementRequest(Employee Employee, IEnumerable<CalculatePayrollPayStatement>? PreviousPayStatements = null);
}
