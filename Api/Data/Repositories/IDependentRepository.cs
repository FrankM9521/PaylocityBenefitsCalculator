using Api.BusinessLogic.Models;

namespace Api.Data.Repositories
{
    public interface IDependentRepository : IGetRepositoryBase<Dependent>, ICreateRepositoryBase<Dependent>
    {
        Task<IEnumerable<Dependent>> GetByEmployeeID(int employeeID);
    }
}
