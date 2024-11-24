using Api.Api.Utility;
using Api.BusinessLogic.Models;
using Api.Data;

namespace Api.BusinessLogic.Validation
{
    public class ValidateEmployeeHasLessThan26Checks : IValidate<Employee>
    {
        private readonly IDbContextAccessor _dbContextAccessor;
        private readonly IBenefitsConfig _benefitsConfig;
        public ValidateEmployeeHasLessThan26Checks(IDbContextAccessor dbContextAccessor, IBenefitsConfig benefitsConfig)
        {
            _dbContextAccessor = dbContextAccessor;
            _benefitsConfig = benefitsConfig;   
        }
        public async Task<ValidationResponse> Validate(Employee value)
        {
            var canAddStatements = _dbContextAccessor.DataBase.PayStatements.Count(pay => pay.EmployeeID == value.Id) < _benefitsConfig.PAY_PERIODS_PER_YEAR;

            return new ValidationResponse(canAddStatements, canAddStatements ? "" : $"User has been paid for the year {_benefitsConfig.PAY_PERIODS_PER_YEAR} times!!!");

        }
    }
}
