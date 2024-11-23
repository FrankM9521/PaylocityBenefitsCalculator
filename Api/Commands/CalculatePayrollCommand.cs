using Api.Repositories;
using MediatR;

namespace Api.Commands
{
    public class CalculatePayrollCommand : IRequest<CalculatePayrollCommandResponse>
    {
        public int EmployeeID { get; set; }
        public decimal HourlyRate { get; set; } = 0M;
        public decimal YearlySalary { get; set; } = 0M;
    }
}
