using Paylocity.PayChecks.DataContext.Interfaces;
using Paylocity.PayChecks.Models;
using Paylocity.PayChecks.Repositories.Interfaces;
using Paylocity.Shared.Mappers.EntityDomain;
using Paylocity.Shared.Models;

namespace Paylocity.PayChecks.Repositoies
{
    public class PayCheckRepository : IPayCheckRepository
    {
        private IPayChecksDbContextAccessor _dbContexAccessor;
        public PayCheckRepository(IPayChecksDbContextAccessor dbContextAccessor)
        {
            _dbContexAccessor = dbContextAccessor;
        }

        public async Task<CreateObjectResponse> Create(PayCheck value)
        {
            _dbContexAccessor.Add(value.ToEntity());

            return await Task.FromResult(new CreateObjectResponse(value.ID));
        }

        public async Task<IEnumerable<PayCheck>?> GetByEmployeeID(int employeeID)
        {
            return await Task.FromResult(_dbContexAccessor.PayChecks
                        .Where(pay => pay.EmployeeID == employeeID)
                        .Select(s => s.ToModel()));
        }

        public async Task<int> GetPaycheckCount(int employeeID)
        {
            return  _dbContexAccessor.PayChecks.Count(pay => pay.EmployeeID == employeeID);
        }
    }
}
