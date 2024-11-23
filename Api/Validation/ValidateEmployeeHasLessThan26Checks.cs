using Api.Models;
using Api.Repositories;

namespace Api.Validation
{
    public class ValidateEmployeeHasLessThan26Checks : IValidate<Employee>
    {
        public async Task<ValidationResponse> Validate(Employee value)
        {
            var canAddStatements = DB.PayStatements.Count(pay => pay.EmployeeID == value.Id) < Constants.PAY_PERIODS_PER_YEAR;

            return new ValidationResponse(canAddStatements, canAddStatements ? "" : "User has been paid for the year.");

        }
        private DataBase DB
        {
            get { return DataBase.Instance; }
        }
    }
}
