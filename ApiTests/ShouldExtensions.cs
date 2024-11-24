using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Api.Api.Dtos.Dependent;
using Api.BusinessLogic.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Xunit;

namespace ApiTests;

internal static class ShouldExtensions
{
    public static Task ShouldReturn(this HttpResponseMessage response, HttpStatusCode expectedStatusCode)
    {
        AssertCommonResponseParts(response, expectedStatusCode);
        return Task.CompletedTask;
    }
    
    public static async Task ShouldReturn<T>(this HttpResponseMessage response, HttpStatusCode expectedStatusCode, T expectedContent)
    {
        await response.ShouldReturn(expectedStatusCode);
        Assert.Equal("application/json", response.Content.Headers.ContentType?.MediaType);
        var apiResponse = JsonConvert.DeserializeObject<ApiResponse<T>>(await response.Content.ReadAsStringAsync());
        Assert.True(apiResponse.Success);
        Assert.Equal(JsonConvert.SerializeObject(expectedContent), JsonConvert.SerializeObject(apiResponse.Data));
    }

    private static void AssertCommonResponseParts(this HttpResponseMessage response, HttpStatusCode expectedStatusCode)
    {
        Assert.Equal(expectedStatusCode, response.StatusCode);
    }
}

public static class AssertExtensions
{
    public static void AssertEqual<T>(this ObjectResult result, ApiResponse<IEnumerable<T>> expected)
    {
        var apiResponse = result.Value as ApiResponse<List<T>>;

        Assert.NotNull(apiResponse);
        Assert.Equal(JsonConvert.SerializeObject(expected), JsonConvert.SerializeObject(apiResponse));
    }
    public static void AssertEqual<T>(this ObjectResult result, ApiResponse<T> expected)
    {
        var apiResponse = result.Value as ApiResponse<T>;

        Assert.NotNull(apiResponse);
        Assert.Equal(JsonConvert.SerializeObject(expected), JsonConvert.SerializeObject(apiResponse));
    }
}
