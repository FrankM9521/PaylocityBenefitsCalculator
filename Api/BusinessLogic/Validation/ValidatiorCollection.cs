namespace Api.BusinessLogic.Validation
{
    public record ValidationResponse(bool Success = true, string ErrorMessage = "") { }

    public interface IValidationCollection<T>
    {
        void Add(IValidate<T> validator);
        Task<ValidationResponse> Validate(T value);
    }

    public abstract class ValidatiorCollection<T>
    {
        public List<IValidate<T>> _validators = new();

        public void Add(IValidate<T> validator)
        {
            _validators.Add(validator);
        }

        public async Task<ValidationResponse> Validate(T value)
        {
            var errorMessages = new List<string>();
            foreach (var validator in _validators)
            {
                var result = await validator.Validate(value);

                if (!result.Success)
                {
                    errorMessages.Add(result.ErrorMessage);
                }
            }

            return new ValidationResponse(errorMessages.Count == 0, string.Join("", errorMessages));
        }
    }
}
