using Api.BusinessLogic.Mappers;
using Api.BusinessLogic.Models;
using Api.BusinessLogic.Models.Response;
using Api.BusinessLogic.Validation;
using Api.Data.Repositories.Interfaces;
using System.Net;

namespace Api.Data.Repositories
{
    public class DependentRepository : RepositoryBase,  IDependentRepository
    {
        private readonly IValidationCollection<Dependent> _dependentValidationCollection;

        public DependentRepository(IValidationCollection<Dependent> dependentValidationCollection, IDbContextAccessor dbContextAccessor) : base(dbContextAccessor)
        {
            _dependentValidationCollection = dependentValidationCollection;
            _dbContexAccessor = dbContextAccessor;
        }

        public async Task<IEnumerable<Dependent>> Get()
        {
            return await Task.FromResult(DataComtext.Dependents.Select(d => d.ToDomain())) ?? new List<Dependent>();
        }

        public async Task<Dependent?> GetByID(int id)
        {
            var entity = DataComtext.Dependents.FirstOrDefault(emp => emp.Id == id);

            return entity != null ? await Task.FromResult(entity.ToDomain()) : null;
        }
        public async Task<IEnumerable<Dependent>> GetByEmployeeID(int employeeID)
        {
            return await Task.FromResult(DataComtext.Dependents.Where(dep => dep.EmployeeId == employeeID).Select(dep => dep.ToDomain()))
                ?? new List<Dependent>();
        }
        public async Task<CreateObjectResponse> Create(Dependent newDependent)
        {
            var result = await _dependentValidationCollection.Validate(newDependent);

            if (result.Success)
            {
                // next id
                newDependent.Id = DataComtext.Dependents.Max(d => d.Id) + 1;

                DataComtext.Add(newDependent.ToEntity());

                return new CreateObjectResponse(newDependent.Id);
            }
            else
            {
                return new CreateObjectResponse(null, false, HttpStatusCode.BadRequest, result.ErrorMessage);
            }
        }
    }
}
