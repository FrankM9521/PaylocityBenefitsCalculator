using Api.BusinessLogic.Models;
using Api.Data;
using Api.Data.Entities;

namespace Api.BusinessLogic.Validation
{
    public class ValidateDependentOnlyHasOneSpouseOrDomesticPartner : IValidate<Dependent>
    {
        private readonly IDbContextAccessor _dbContextAccessor;
        public ValidateDependentOnlyHasOneSpouseOrDomesticPartner(IDbContextAccessor dbContextAccessor)
        {
            _dbContextAccessor = dbContextAccessor;
        }

        public async Task<ValidationResponse> Validate(Dependent value)
        {
            var uniqueRelationships = new List<RelationshipType> { RelationshipType.Spouse, RelationshipType.DomesticPartner };
            var isValid = uniqueRelationships.Contains((RelationshipType)value.Relationship)
                ? !_dbContextAccessor.DataBase.Dependents.Any(dep => dep.EmployeeId == value.EmployeeId && uniqueRelationships.Contains(dep.Relationship))
                : true;

            return new ValidationResponse(isValid, isValid ? ""  : $"Cannot Have More than One {string.Join(",", uniqueRelationships)}");
        }
    }
}
