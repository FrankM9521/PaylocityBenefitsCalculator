using Paylocity.Employees.Models;
using Paylocity.PayChecks.Repositories.Interfaces;
using Paylocity.Shared.Config;
using Paylocity.Validation.Models;
using Paylocity.ValidationLibrary.Interfaces;

namespace Paylocity.Validation.Validation
{
    public class ValidateEmployeeHasLessThan26Checks : IValidate<Employee>
    {
        private readonly IPayCheckRepository _dbContextAccessor;
        private readonly IBenefitsConfig _benefitsConfig;
        public ValidateEmployeeHasLessThan26Checks(IPayCheckRepository dbContextAccessor, IBenefitsConfig benefitsConfig)
        {
            _dbContextAccessor = dbContextAccessor;
            _benefitsConfig = benefitsConfig;
        }
        public async Task<ValidationResponse> Validate(Employee value)
        {
            var canAddStatements = await _dbContextAccessor.GetPaycheckCount(value.Id) < _benefitsConfig.PAY_PERIODS_PER_YEAR;

            return new ValidationResponse(canAddStatements, canAddStatements ? "" : $"User has been paid for the year {_benefitsConfig.PAY_PERIODS_PER_YEAR} times!!!");
        }
    }
}
