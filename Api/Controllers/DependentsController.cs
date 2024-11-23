using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Mappers;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Runtime.CompilerServices;

namespace Api.Controllers;

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
    public async Task<IActionResult> Get(int id)
    {
        var result = await _dependentService.GetByID(id);

        return result == null
            ? MyNotFound<GetDependentDto>()
            : MyOk(result.ToDto());
    }

    [SwaggerOperation(Summary = "Get all dependents")]
    [HttpGet("")]
    public async Task<IActionResult> GetAll()
    {
        var results = await _dependentService.GetAll();

        return MyOk(results.Select(s => s.ToDto()).ToList());
    }

    [SwaggerOperation(Summary = "Create a new dependents")]
    [HttpPost("")]
    public async Task<IActionResult> Post(GetDependentDto dependent)
    {
        var result = await _dependentService.Post(dependent.ToModel());

        return result.Success
            ? CreatedAt<GetDependentDto>(result.newId.Value, Request.Path.Value)
            : MyBadRequest<GetDependentDto>(result.ErrorMessage);
    }
}
