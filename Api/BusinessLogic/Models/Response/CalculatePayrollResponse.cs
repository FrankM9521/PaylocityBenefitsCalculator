using Api.BusinessLogic.Models.CalculatePayroll;

namespace Api.BusinessLogic.Models.Response
{
    public record CalculateCheckResponse(CalculatePayrollEmployee Employee, CalculatePayrollStatement PayCheck);
}
