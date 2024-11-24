using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Api.Api.Controllers;
using Api.Api.Dtos.Dependent;
using Api.Api.Dtos.Employee;
using Api.Api.Utility;
using Api.BusinessLogic.Mappers;
using Api.BusinessLogic.Models;
using Api.BusinessLogic.Models.Response;
using Api.BusinessLogic.Services;
using Api.BusinessLogic.Validation;
using Api.Data;
using Api.Data.Repositories;
using Api.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace ApiTests.IntegrationTests;

public class EmployeeIntegrationTests 
{
    private readonly EmployeesController _employeesController;
    private readonly IEmployeeService _employeeService;
    private readonly IEmployeeRepository _employeeRepository;
    public EmployeeIntegrationTests()
    {
        var dbContextAccessor = new DbContextAccessor();
        _employeeService = new EmployeeService
                (new EmployeeRepository(dbContextAccessor), new DependentRepository
                    (new DependentValidationCollection(dbContextAccessor), dbContextAccessor));

        _employeesController = new EmployeesController(_employeeService); 
    }

    [Fact]
    public async Task WhenAskedForAllEmployees_ShouldReturnAllEmployees()
    {
        var employees= await _employeeService.GetAll();
        var expectedResponse = new ApiResponse<IEnumerable<GetEmployeeDto>>
        {
            Data = employees.Select(emp => emp.ToDto()).ToList(),
            Success = true,
            HttpStatusCode = HttpStatusCode.OK,
            Message = ""
        };

        var actionResult = await _employeesController.GetAll() as ObjectResult;

        actionResult.AssertEqual(expectedResponse);
    }

    [Fact]
    public async Task WhenAskedForAnEmployee_ShouldReturnCorrectEmployee()
    {
        var expectedEmployee = await _employeeService.GetByID(1);
        var expectedResponse = new ApiResponse<GetEmployeeDto>
        {
            Data = expectedEmployee.ToDto(),
            Success = true,
            HttpStatusCode = HttpStatusCode.OK,
            Message = ""
        };

        var actionResult = await _employeesController.Get(expectedEmployee.Id) as ObjectResult;

        actionResult.AssertEqual<GetEmployeeDto>(expectedResponse);
    }
    
    [Fact]
    public async Task WhenAskedForANonexistentEmployee_ShouldReturn404()
    {
        var expectedResponse = new ApiResponse<GetEmployeeDto>
        {
            Data = null,
            Success = false,
            HttpStatusCode = HttpStatusCode.NotFound,
            Message = "Not Found",
            Error = "Not Found"
        };

        var actionResult = await _employeesController.Get(785) as ObjectResult;

        actionResult.AssertEqual(expectedResponse);
    }
}

