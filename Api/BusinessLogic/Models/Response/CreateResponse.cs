using System.Net;

namespace Api.BusinessLogic.Models.Response
{
    public record CreateResponse(int? newId, bool Success = true, HttpStatusCode HttpStatusCode = HttpStatusCode.Created, string ErrorMessage = "");
}
