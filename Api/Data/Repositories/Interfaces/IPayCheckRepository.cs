using Api.BusinessLogic.Models;

namespace Api.Data.Repositories.Interfaces
{
    public interface IPayCheckRepository : ICreateRepository<PayCheck>
    {
        Task<IEnumerable<PayCheck>?> GetByEmployeeID(int employeeID);
    }
}
