using Api.BusinessLogic.Models.CalculatePayroll;

namespace Api.BusinessLogic.Models.Response
{
    public record CalculatePayrollResponse(CalculatePayrollEmployee Employee, CalculatePayrollStatement PayCheck);
}
