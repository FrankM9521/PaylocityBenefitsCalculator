using Api.BusinessLogic.Models;

namespace Api.Data.Repositories.Interfaces
{
    public interface IDependentRepository : IGetRepository<Dependent>, ICreateRepository<Dependent>
    {
        Task<IEnumerable<Dependent>> GetByEmployeeID(int employeeID);
    }
}
