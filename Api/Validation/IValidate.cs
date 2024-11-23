using Api.Repositories;

namespace Api.Validation
{
    public interface IValidate<T>
    {
        Task<ValidationResponse> Validate(T value);
    }
}
