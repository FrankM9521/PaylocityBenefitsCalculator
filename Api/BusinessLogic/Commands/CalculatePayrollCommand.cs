using MediatR;

namespace Api.BusinessLogic.Commands
{
    public record CalculatePayrollCommand(int EmployeeID) : IRequest<CalculatePayrollCommandResponse>;
}
