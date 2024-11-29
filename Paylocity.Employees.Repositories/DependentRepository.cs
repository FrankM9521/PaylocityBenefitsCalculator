using Paylocity.Employees.DataContext.Interfaces;
using Paylocity.Employees.Models;
using Paylocity.Employees.Repositories.Interfaces;
using Paylocity.Shared.Mappers.EntityDomain;
using Paylocity.Shared.Models;

namespace Paylocity.Employees.Repositories
{
    public class DependentRepository : IDependentRepository
    {
        private readonly IEmployeesDbContextAccessor _employeesDbContextAccessor;
        public DependentRepository(IEmployeesDbContextAccessor employeesDbContextAccessor)
        {
            _employeesDbContextAccessor = employeesDbContextAccessor;
        }
        public Task<CreateObjectResponse> Create(Dependent value)
        {
              _employeesDbContextAccessor.Add(value.ToEntity());

            return Task.FromResult(new CreateObjectResponse(value.Id));
        }

        public async Task<IEnumerable<Dependent>> Get()
        {
            return  _employeesDbContextAccessor.Dependents.Select(dep => dep.ToDomain());
        }

        public async Task<IEnumerable<Dependent>> GetByEmployeeID(int employeeID)
        {
            return (_employeesDbContextAccessor.Dependents.Where(dep => dep.EmployeeId ==  employeeID)).Select(dep => dep.ToDomain());    
        }

        public async Task<Dependent?> GetByID(int id)
        {
            return  _employeesDbContextAccessor.Dependents.FirstOrDefault(dep => dep.Id == id)?.ToDomain();
        }

        public async Task<bool> RelationshipExists(Dependent dep, List<Relationship> relationshipsToCheck)
        {
            return relationshipsToCheck.Contains(dep.Relationship)
                ? !_employeesDbContextAccessor.Dependents.Any(dep => dep.EmployeeId == dep.EmployeeId 
                    && relationshipsToCheck.Contains((Relationship)dep.Relationship))
                : true;
        }
    }
}
