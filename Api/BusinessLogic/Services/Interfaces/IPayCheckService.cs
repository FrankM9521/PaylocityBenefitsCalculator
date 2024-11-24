using Api.BusinessLogic.Models;
using Api.Data.Repositories.Interfaces;

namespace Api.BusinessLogic.Services.Interfaces
{
    public interface IPayCheckService : ICreateRepository<PayCheck>
    {
        Task<IEnumerable<PayCheck>> GetByEmployeeID(int employeeID);
    }
}
