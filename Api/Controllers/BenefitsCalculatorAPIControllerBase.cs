using Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Controllers;

public abstract class BenefitsCalculatorAPIControllerBase: ControllerBase
{
    
    public IActionResult MyOk<T>(T data)
    {
        return Ok(new ApiResponse<T>(data));
    }

    public IActionResult MyOk<T>(List<T> data)
    {
        return Ok(new ApiResponse<List<T>>(data));
    }

    public IActionResult MyBadRequest<T>(string errorMessage)
    {
        return Ok(new ApiResponse<T>(default(T), HttpStatusCode.BadRequest, false, "Bad Request", errorMessage));
    }

    public IActionResult CreatedAt<T>(int id, string urlRoot)
    {
        return Ok(new ApiResponse<T>(default(T), HttpStatusCode.Created, true, $"Created At: {urlRoot}/{id}"));
    }

    public  IActionResult MyNotFound<T>()
    {
        return Ok(new ApiResponse<T>(default(T), HttpStatusCode.NotFound, false, "Not Found", "Not Found"));
    }
}

public class Link
{
    public string Method { get; init; }
    public string Url { get; init; }
}
