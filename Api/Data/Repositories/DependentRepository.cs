using Api.BusinessLogic.Mappers;
using Api.BusinessLogic.Models;
using Api.BusinessLogic.Models.Response;
using Api.BusinessLogic.Validation;
using System.Net;

namespace Api.Data.Repositories
{
    public class DependentValidationCollection : ValidatiorCollection<Dependent>, IValidationCollection<Dependent>
    {
        public DependentValidationCollection()
        {
            _validators.Add(new ValidateDependentOnlyHasOneSpouseOrDomesticPartner());
        }
    }

    public class EmployeeValidationCollection : ValidatiorCollection<Employee>, IValidationCollection<Employee>
    {
        public EmployeeValidationCollection()
        {
            _validators.Add(new ValidateEmployeeHasLessThan26Checks());
        }
    }

    public class DependentRepository : IDependentRepository
    {
        private readonly IValidationCollection<Dependent> _dependentValidationCollection;

        public DependentRepository(IValidationCollection<Dependent> dependentValidationCollection)
        {
            _dependentValidationCollection = dependentValidationCollection;
        }

        public async Task<IEnumerable<Dependent>> Get()
        {
            return await Task.FromResult(DB.Dependents.Select(d => d.ToDomain())) ?? new List<Dependent>();
        }

        public async Task<Dependent?> GetByID(int id)
        {
            var entity = DB.Dependents.FirstOrDefault(emp => emp.Id == id);

            return entity != null ? await Task.FromResult(entity.ToDomain()) : null;
        }
        public async Task<IEnumerable<Dependent>> GetByEmployeeID(int employeeID)
        {
            return await Task.FromResult(DB.Dependents.Where(dep => dep.EmployeeId == employeeID).Select(dep => dep.ToDomain()))
                ?? new List<Dependent>();
        }
        public async Task<CreateResponse> Create(Dependent newDependent)
        {
            var result = await _dependentValidationCollection.Validate(newDependent);

            if (result.Success)
            {
                // next id
                newDependent.Id = DB.Dependents.Max(d => d.Id) + 1;

                DB.Add(newDependent.ToEntity());

                return new CreateResponse(newDependent.Id);
            }
            else
            {
                return new CreateResponse(null, false, HttpStatusCode.BadRequest, result.ErrorMessage);
            }
        }



        private DataBase DB
        {
            get { return DataBase.Instance; }
        }
    }
}
