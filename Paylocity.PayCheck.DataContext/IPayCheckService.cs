using Paylocity.PayChecks.Models;
using Paylocity.Shared.Repositories.Interfaces;

namespace Paylocity.PayChecks.Services.Interfaces
{
    public interface IPayCheckService : ICreateRepository<PayCheck>
    {
        Task<IEnumerable<PayCheck>> GetByEmployeeID(int employeeID);
    }
}
