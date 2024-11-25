using Api.BusinessLogic.Models;
using Api.BusinessLogic.Models.Request;
using Api.BusinessLogic.Services;
using Api.BusinessLogic.Services.Interfaces;
using Api.BusinessLogic.Validation;
using MediatR;

namespace Api.BusinessLogic.Commands
{
    /// <summary>
    /// Manages a service that creates a Pay Check
    /// </summary>
    public class CalculatePayCheckCommandHandler : IRequestHandler<CalculatePayCheckCommand, CalculatePayCheckCommandResponse>
    {
        private readonly IValidationCollection<Employee> _employeeValidationCollection;
        private readonly IEmployeeService _employeeService;
        private readonly ICalculatePayCheckService _payStatementFactory;
        private readonly IPayCheckService _payCheckService;
        public CalculatePayCheckCommandHandler(IEmployeeService employeeService,
            IValidationCollection<Employee> employeeValidationCollection,
            ICalculatePayCheckService payStatementFactory,
            IPayCheckService payCheckService)
        {
            _employeeService = employeeService;
            _payStatementFactory = payStatementFactory;
            _employeeValidationCollection = employeeValidationCollection;
            _payCheckService = payCheckService;
        }

        /// <summary>
        /// Creates a Pay Check
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<CalculatePayCheckCommandResponse> Handle(CalculatePayCheckCommand request, CancellationToken cancellationToken)
        {
            var employee = await _employeeService.GetByID(request.EmployeeID);

            if (employee == null)
            {
                return new CalculatePayCheckCommandResponse(null, null, false, "Not Found");
            }

            var validationResult = await _employeeValidationCollection.Validate(employee);

            if (validationResult.Success)
            {
                var previousStatements = await _payCheckService.GetByEmployeeID(request.EmployeeID);
                var createResult = await _payStatementFactory.Calculate(new CalculatePayrollRequest(employee, previousStatements));

                return new CalculatePayCheckCommandResponse(createResult.Employee, createResult.PayCheck);
            }

            return new CalculatePayCheckCommandResponse(null, null, false, validationResult.ErrorMessage);
        }
    }
}

