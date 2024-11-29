using System.Net;

namespace Paylocity.Shared.Dto;

public record ApiResponse<T>(T? Data = default, HttpStatusCode HttpStatusCode = HttpStatusCode.OK, bool Success = true, string Message = "", string Error = "")
{
}
