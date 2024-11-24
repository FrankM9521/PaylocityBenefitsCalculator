using System.Net;

namespace Api.BusinessLogic.Models.Response;

public record ApiResponse<T>(T? Data = default, HttpStatusCode HttpStatusCode = HttpStatusCode.OK, bool Success = true, string Message = "", string Error = "")
{
}
