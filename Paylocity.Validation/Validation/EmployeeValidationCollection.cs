using Paylocity.Employees.Models;
using Paylocity.PayChecks.Repositories.Interfaces;
using Paylocity.Shared.Config;
using Paylocity.Validation.Models;
using Paylocity.ValidationLibrary.Interfaces;

namespace Paylocity.Validation.Validation
{
    public class EmployeeValidationCollection : ValidatiorCollection<Employee>, IValidationCollection<Employee>
    {
        public EmployeeValidationCollection(IPayCheckRepository dbContextAccessor, IBenefitsConfig benefitsConfig)
        {
            _validators.Add(new ValidateEmployeeHasLessThan26Checks(dbContextAccessor, benefitsConfig));
        }

        public async Task<ValidationResponse> Validate(Employee value)
        {
            foreach (var validator in _validators)
            {
                var validationResponse = await validator.Validate(value);
                if (validationResponse != null && !validationResponse.Success)
                {
                    return validationResponse;
                }
            }

            return new ValidationResponse();
        }

        Task<ValidationResponse> IValidationCollection<Employee>.Validate(Employee value)
        {
            throw new NotImplementedException();
        }
    }
}
