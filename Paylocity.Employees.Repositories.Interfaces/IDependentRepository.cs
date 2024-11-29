
using Paylocity.Employees.Models;
using Paylocity.Shared.Repositories.Interfaces;

namespace Paylocity.Employees.Repositories.Interfaces
{
    public interface IDependentRepository :  IGetRepository<Dependent>,  ICreateRepository<Dependent>
    {
        Task<IEnumerable<Dependent>> GetByEmployeeID(int employeeID);
        Task<bool> RelationshipExists(Dependent dep, List<Relationship> relationshipsToCheck);
    }
}
