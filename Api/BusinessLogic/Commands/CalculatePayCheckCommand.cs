using MediatR;

namespace Api.BusinessLogic.Commands
{
    public record CalculatePayCheckCommand(int EmployeeID) : IRequest<CalculatePayCheckCommandResponse>;
}
