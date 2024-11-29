
using Paylocity.PayChecks.Models;
using Paylocity.Shared.Repositories.Interfaces;

namespace Paylocity.PayChecks.Repositories.Interfaces
{
    public interface IPayCheckRepository : ICreateRepository<PayCheck>
    {
        Task<IEnumerable<PayCheck>?> GetByEmployeeID(int employeeID);

        Task<int> GetPaycheckCount(int employeeID);
    }
}
