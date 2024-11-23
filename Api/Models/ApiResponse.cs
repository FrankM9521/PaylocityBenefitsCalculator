using System.Net;

namespace Api.Models;

// Changed to record to keep object off the managed heap (no GC)
// Some props are init once, 
public record ApiResponse<T>(T? Data = default,  HttpStatusCode HttpStatusCode = HttpStatusCode.OK,   bool Success = true, string Message = "", string Error = "")
{
}
