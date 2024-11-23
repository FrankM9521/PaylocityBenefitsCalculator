namespace Api.BusinessLogic.Validation
{
    public interface IValidate<T>
    {
        Task<ValidationResponse> Validate(T value);
    }
}
