using Paylocity.Employees.DataContext.Interfaces;
using Paylocity.Employees.Models;
using Paylocity.Employees.Repositories.Interfaces;
using Paylocity.Validation.Models;
using Paylocity.ValidationLibrary.Interfaces;

namespace Paylocity.Validation.Validation
{
    public class DependentValidationCollection : ValidatiorCollection<Dependent>, IValidationCollection<Dependent>
    {
        public DependentValidationCollection(IDependentRepository dependentRepository)
        {
            _validators.Add(new ValidateDependentOnlyHasOneSpouseOrDomesticPartner(dependentRepository));
        }

        public async Task<ValidationResponse>  Validate(Dependent value)
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
    }
}
