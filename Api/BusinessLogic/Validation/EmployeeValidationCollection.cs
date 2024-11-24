using Api.Api.Utility;
using Api.BusinessLogic.Models;
using Api.Data;

namespace Api.BusinessLogic.Validation
{
    public class EmployeeValidationCollection : ValidatiorCollection<Employee>, IValidationCollection<Employee>
    {
        public EmployeeValidationCollection(IDbContextAccessor dbContextAccessor, IBenefitsConfig benefitsConfig)
        {
            _validators.Add(new ValidateEmployeeHasLessThan26Checks(dbContextAccessor, benefitsConfig));
        }
    }
}
