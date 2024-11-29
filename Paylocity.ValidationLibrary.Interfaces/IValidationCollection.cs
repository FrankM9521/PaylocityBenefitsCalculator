using Paylocity.Validation.Models;

namespace Paylocity.ValidationLibrary.Interfaces
{
    public interface IValidationCollection<T>
    {
        void Add(IValidate<T> validator);
        Task<ValidationResponse> Validate(T value);
    }
}
