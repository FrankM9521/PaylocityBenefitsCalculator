using Api.Enitiies;
using Api.Models;
using Api.Repositories;

namespace Api.Validation
{
    public class ValidateDependentOnlyHasOneSpouseOrDomesticPartner : IValidate<Dependent>
    {
        public async Task<ValidationResponse> Validate(Dependent value)
        {
            var uniqueRelationships = new List<RelationshipType> { RelationshipType.Spouse, RelationshipType.DomesticPartner };

            var existing = DB.Dependents.FirstOrDefault(dep => dep.EmployeeId == value.EmployeeId && uniqueRelationships.Contains(dep.Relationship));

            return new ValidationResponse(existing == null, 
                existing == null
                ?  ""
                : "Cannot Have Moe than One Spouse or Domestic Partner");
        }

        private DataBase DB
        {
            get { return DataBase.Instance; }
        }
    }
}
