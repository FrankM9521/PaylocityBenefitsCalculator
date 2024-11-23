using Api.BusinessLogic.Factories;
using Api.BusinessLogic.Mappers;
using Api.BusinessLogic.Models;
using Api.BusinessLogic.Services;
using Api.BusinessLogic.Validation;
using Api.Data.Repositories;
using MediatR;

namespace Api.BusinessLogic.Commands
{
    public class CalculatePayrollCommandHandler : IRequestHandler<CalculatePayrollCommand, CalculatePayrollCommandResponse>
    {
        private readonly IValidationCollection<Employee> _employeeValidationCollection;
        private readonly IEmployeeService _employeeService;
        private readonly IFactory<CreatePayStatmentResponse, CreatePayStatementRequest> _payStatementFactory;
        public CalculatePayrollCommandHandler(IEmployeeService employeeService,
             IValidationCollection<Employee> employeeValidationCollection,
            IFactory<CreatePayStatmentResponse, CreatePayStatementRequest> payStatementFactory)
        {
            _employeeService = employeeService;
            _payStatementFactory = payStatementFactory;
            _employeeValidationCollection = employeeValidationCollection;
        }
        public async Task<CalculatePayrollCommandResponse> Handle(CalculatePayrollCommand request, CancellationToken cancellationToken)
        {
            var employee = await _employeeService.GetByID(request.EmployeeID);

            if (employee == null)
            {
                return new CalculatePayrollCommandResponse(null, null, false, "Not Found");
            }

            var validationResult = await _employeeValidationCollection.Validate(employee);

            if (validationResult.Success)
            {
                var previousStatements = DB.PayStatements.Select(pay => pay.ToModel(employee.Dependents.Count()));
                var createPayStatementRequest = new CreatePayStatementRequest(employee, previousStatements);
                var createResult = await _payStatementFactory.Create(createPayStatementRequest);

                return new CalculatePayrollCommandResponse(createResult.Employee, createResult.PayCheck);
            }

            return new CalculatePayrollCommandResponse(null, null, false, validationResult.ErrorMessage);
        }
        private DataBase DB
        {
            get { return DataBase.Instance; }
        }
    }
}

