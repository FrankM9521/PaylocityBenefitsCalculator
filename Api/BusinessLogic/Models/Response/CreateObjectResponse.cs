using System.Net;

namespace Api.BusinessLogic.Models.Response
{
    public record CreateObjectResponse(object? newId, bool Success = true, HttpStatusCode HttpStatusCode = HttpStatusCode.Created, string ErrorMessage = "");
}
