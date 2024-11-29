using System.Net;

namespace Paylocity.Shared.Models
{
    public record CreateObjectResponse(object? newId, bool Success = true, HttpStatusCode HttpStatusCode = HttpStatusCode.Created, string ErrorMessage = "");
}
