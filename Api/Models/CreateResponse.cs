using System.Net;

namespace Api.Models
{
    public record CreateResponse(int? newId, bool Success = true, HttpStatusCode HttpStatusCode = HttpStatusCode.Created, string ErrorMessage = "")
    {

    }
}
