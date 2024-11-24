using Api.BusinessLogic.Models;
using Api.Data;

namespace Api.BusinessLogic.Validation
{
    public class DependentValidationCollection : ValidatiorCollection<Dependent>, IValidationCollection<Dependent>
    {
        public DependentValidationCollection(IDbContextAccessor dbContextAccessor)
        {
            _validators.Add(new ValidateDependentOnlyHasOneSpouseOrDomesticPartner(dbContextAccessor));
        }
    }
}
