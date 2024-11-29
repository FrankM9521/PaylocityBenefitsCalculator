using Microsoft.AspNetCore.Mvc;
using Paylocity.Employees.Dtos.Employee;
using Paylocity.Employees.Services;
using Paylocity.Shared.Dto;
using Paylocity.Shared.Mappers.DomainDto;
using Swashbuckle.AspNetCore.Annotations;

namespace Paylocity.Employees.Api.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class EmployeesController : BenefitsCalculatorAPIControllerBase
{
    private readonly IEmployeeService _employeeService;
    public EmployeesController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [SwaggerOperation(Summary = "Get employee by id")]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<GetEmployeeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<GetEmployeeDto>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _employeeService.GetByID(id);

        return result == null
            ? MyNotFound<GetEmployeeDto>()
            : MyOk(result.ToDto());
    }

    [SwaggerOperation(Summary = "Get all employees")]
    [HttpGet("")]
    [ProducesResponseType(typeof(ApiResponse<List<GetEmployeeDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<List<GetEmployeeDto>>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAll()
    {
        var results = await _employeeService.GetAll();

        return MyOk(results.Select(s => s.ToDto()).ToList());
    }
}
