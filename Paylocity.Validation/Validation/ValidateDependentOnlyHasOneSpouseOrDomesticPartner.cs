using Paylocity.Employees.Models;
using Paylocity.Employees.Repositories.Interfaces;
using Paylocity.Validation.Models;
using Paylocity.ValidationLibrary.Interfaces;

namespace Paylocity.Validation.Validation
{
    public class ValidateDependentOnlyHasOneSpouseOrDomesticPartner : IValidate<Dependent>
    {
        private readonly IDependentRepository _dependentRepository;
        public ValidateDependentOnlyHasOneSpouseOrDomesticPartner(IDependentRepository dependentRepository)
        {
            _dependentRepository = dependentRepository;
        }

        public async Task<ValidationResponse> Validate(Dependent value)
        {
            var uniqueRelationships = new List<Relationship> { Relationship.Spouse, Relationship.DomesticPartner }.ToList();
            
            var exists =await  _dependentRepository.RelationshipExists(value, uniqueRelationships);
            
            return new ValidationResponse(!exists, !exists ? "" : $"Cannot Have More than One {string.Join(",", uniqueRelationships)}");
        }
    }
}
