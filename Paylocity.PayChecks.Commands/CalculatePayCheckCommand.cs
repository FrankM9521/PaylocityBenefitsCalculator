using MediatR;

namespace Paylocity.PayChecks.Commands
{
    public record CalculatePayCheckCommand(int EmployeeID) : IRequest<CalculatePayCheckCommandResponse>;
}
