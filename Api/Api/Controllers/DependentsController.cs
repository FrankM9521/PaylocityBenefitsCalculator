using Microsoft.AspNetCore.Mvc;
using Paylocity.Employees.Dtos.Dependent;
using Paylocity.Employees.Services.Interfaces;
using Paylocity.Shared.Dto;
using Paylocity.Shared.Mappers.DomainDto;
using Swashbuckle.AspNetCore.Annotations;

namespace Paylocity.Employees.Api.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DependentsController : BenefitsCalculatorAPIControllerBase
{
    private readonly IDependentService _dependentService;

    public DependentsController(IDependentService dependentService)
    {
        _dependentService = dependentService;
    }

    [SwaggerOperation(Summary = "Get dependent by id")]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<GetDependentDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<GetDependentDto>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _dependentService.GetByID(id);

        return result == null
            ? MyNotFound<GetDependentDto>()
            : MyOk(result.ToDto());
    }

    [SwaggerOperation(Summary = "Get all dependents")]
    [HttpGet("")]
    [ProducesResponseType(typeof(ApiResponse<List<GetDependentDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<List<GetDependentDto>>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAll()
    {
        var results = await _dependentService.GetAll();

        return MyOk(results.Select(s => s.ToDto()).ToList());
    }

    /// <summary>
    /// Creates a new dependent, added to test requirement that only one spouse or significant other exists
    /// </summary>
    /// <param name="dependent"></param>
    /// <returns></returns>
    [SwaggerOperation(Summary = "Create a new dependent. If the dependent is a spouse or significant other, and one already exists, will return 400 Bad request")]
    [HttpPost("")]
    [ProducesResponseType(typeof(ApiResponse<GetDependentDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<GetDependentDto>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post(GetDependentDto dependent)
    {
        var result = await _dependentService.Create(dependent.ToModel());

        return result.Success
            ? CreatedAt<GetDependentDto>((int)result.newId, Request.Path.Value)
            : MyBadRequest<GetDependentDto>(result.ErrorMessage);
    }
}
