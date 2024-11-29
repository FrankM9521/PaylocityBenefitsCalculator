using Paylocity.Validation.Models;
using Paylocity.ValidationLibrary.Interfaces;

namespace Paylocity.Validation.Validation
{

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
