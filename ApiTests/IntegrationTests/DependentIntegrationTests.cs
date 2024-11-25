using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Api.Api.Controllers;
using Api.Api.Dtos.Dependent;
using Api.BusinessLogic.Mappers;
using Api.BusinessLogic.Models.Response;
using Api.BusinessLogic.Services;
using Api.BusinessLogic.Validation;
using Api.Data;
using Api.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace ApiTests.IntegrationTests;

/*
 I was having issues with Http Client and web socket issues, so I tested at the controller level
 */
[Collection("CalculatePayCheckCommanHandler")]
public class DependentIntegrationTests 
{
    private readonly DependentsController _dependentsController;

    public DependentIntegrationTests()
    {
        var dbContextAccessor = new DbContextAccessor();
        _dependentsController = new DependentsController
            (new DependentService
                (new DependentRepository
                    (new DependentValidationCollection(dbContextAccessor), dbContextAccessor)));
    }

    [Fact]
    public async Task WhenAskedForAllDependents_ShouldReturnAllDependents()
    {
        var data = DefaultData.Dependents.Select(d => d.ToDomain()).ToList().Select(d => d.ToDto());

        //var response = await HttpClient.GetAsync("/api/v1/dependents");
        var expectedResponse = new ApiResponse<IEnumerable<GetDependentDto>>
        {
            Data = data,
            Success = true,
            HttpStatusCode = HttpStatusCode.OK,
            Message = ""
        };

        var actionResult = await _dependentsController.GetAll() as ObjectResult;
        actionResult.AssertEqual<GetDependentDto>(expectedResponse);
    }

    [Fact]
    public async Task WhenAskedForADependent_ShouldReturnCorrectDependent()
    {
        var expectedDependent = DefaultData.Dependents.Select(d => d.ToDomain()).ToList().Select(d => d.ToDto()).First();
        var expectedResponse = new ApiResponse<GetDependentDto>
        {
            Data = expectedDependent,
            Success = true,
            HttpStatusCode = HttpStatusCode.OK,
            Message = ""
        };

        var actionResult = await _dependentsController.Get(expectedDependent.Id) as ObjectResult;

        actionResult.AssertEqual<GetDependentDto>( expectedResponse);
    }

    [Fact]
    public async Task WhenAskedForANonexistentDependent_ShouldReturn404()
    {
        var expectedResponse = new ApiResponse<GetDependentDto>
        {
            Data = null,
            Success = false,
            HttpStatusCode = HttpStatusCode.NotFound,
            Message = "Not Found",
            Error = "Not Found"
        };

        var actionResult = await _dependentsController.Get(10019) as ObjectResult;

        actionResult.AssertEqual(expectedResponse);
    }

    [Fact]
    public async Task WhenSpouseOrDomesticPartnerExists_ShouldNotCreateAnother()
    {
        var data = new GetDependentDto
        {
            EmployeeId = 2,
            FirstName = "Jada",
            LastName = "Morant",
            Relationship = Api.BusinessLogic.Models.Relationship.Spouse,
            DateOfBirth = System.DateTime.Now.AddDays(-30)
        };

        var expectedResponse = new ApiResponse<GetDependentDto>
        {
            Data = null,
            Success = false,
            HttpStatusCode = HttpStatusCode.BadRequest,
            Message = "Bad Request",
            Error = "Cannot Have More than One Spouse,DomesticPartner"
        };

        var actionResult = await _dependentsController.Post(data) as ObjectResult;

        Assert.NotNull(actionResult);
    }
}

