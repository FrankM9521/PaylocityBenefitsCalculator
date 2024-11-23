using Api.BusinessLogic.Models.CalculatePayroll;

namespace Api.BusinessLogic.Factories
{
    public record CreatePayStatmentResponse(CalculatePayrollEmployee Employee, CalculatePayrollPayStatement PayCheck);
}
