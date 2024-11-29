using Paylocity.Validation.Models;

namespace Paylocity.ValidationLibrary.Interfaces
{
    public interface IValidate<T>
    {
        Task<ValidationResponse> Validate(T value);
    }
}
