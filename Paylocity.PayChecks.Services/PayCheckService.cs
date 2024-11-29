using Paylocity.PayChecks.Models;
using Paylocity.PayChecks.Repositories.Interfaces;
using Paylocity.PayChecks.Services.Interfaces;
using Paylocity.Shared.Models;

namespace Paylocity.PayChecks.Services
{

    public class PayCheckService : IPayCheckService
    {
        private readonly IPayCheckRepository _payCheckRepository;
        public PayCheckService(IPayCheckRepository payCheckRepository)
        {
            _payCheckRepository = payCheckRepository;
        }

        public Task<CreateObjectResponse> Create(PayCheck value)
        {
            // any additional validation could go here
            return _payCheckRepository.Create(value);
        }

        public async Task<IEnumerable<PayCheck>> GetByEmployeeID(int employeeID)
        {
            return await _payCheckRepository.GetByEmployeeID(employeeID) ?? new List<PayCheck>();
        }
    }
}
