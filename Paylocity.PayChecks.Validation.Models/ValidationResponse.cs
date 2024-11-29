namespace Paylocity.Validation.Models
{
    public record ValidationResponse(bool Success = true, string ErrorMessage = "") { }
}
