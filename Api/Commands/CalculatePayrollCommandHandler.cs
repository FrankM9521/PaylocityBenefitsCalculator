using Api.Calculations;
using Api.Mappers;
using Api.Models;
using Api.Repositories;
using Api.Services;
using Api.Validation;
using MediatR;

namespace Api.Commands
{
    public record CalculatePayrollCommandResponse(PayStatement? PayStatement, bool Success = true, string ErrorMessage = "") : ValidationResponse(Success, ErrorMessage)
    {
    }

    public class CalculatePayrollCommandHandler : IRequestHandler<CalculatePayrollCommand, CalculatePayrollCommandResponse>
    {
        private readonly ValidatiorCollection<Employee> _validatiorCollection;
        private readonly IEmployeeService _employeeService;
        private readonly IDeductionCalculatorCollection _deductionCalculators;
        public CalculatePayrollCommandHandler(IEmployeeService employeeService, IDeductionCalculatorCollection deductionCaclulators)
        {
            _employeeService = employeeService;
            _deductionCalculators = deductionCaclulators;
           
            _validatiorCollection = new ValidatiorCollection<Employee>();
            _validatiorCollection.Add(new ValidateEmployeeHasLessThan26Checks());
        }
        public async Task<CalculatePayrollCommandResponse> Handle(CalculatePayrollCommand request, CancellationToken cancellationToken)
        { 
            var employee = await _employeeService.GetByID(request.EmployeeID);

            if (employee == null)
            {
                return new CalculatePayrollCommandResponse(default(PayStatement), false, "Not Found");
            }

            var validationResult = await _validatiorCollection.Validate(employee);
            PayStatement payStatement = null;

            if (validationResult.Success)
            {
                payStatement = PayStatementFactory.Create(employee, request);

                foreach(var calculator in _deductionCalculators.Deductions)
                {
                    payStatement = await calculator.CalculateDeduction(payStatement);
                }

                DB.Add(payStatement.ToEntity());
            }
            return new CalculatePayrollCommandResponse(payStatement, validationResult.Success, validationResult.ErrorMessage);
        }
        private DataBase DB
        {
            get { return DataBase.Instance; }
        }
    }
}
