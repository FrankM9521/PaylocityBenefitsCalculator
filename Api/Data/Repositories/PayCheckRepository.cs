using Api.BusinessLogic.Mappers;
using Api.BusinessLogic.Models;
using Api.BusinessLogic.Models.Response;
using Api.Data.Repositories.Interfaces;

namespace Api.Data.Repositories
{

    public class PayCheckRepository : RepositoryBase,  IPayCheckRepository
    {
        private readonly IDependentRepository _dependentRepository;
        public PayCheckRepository(IDbContextAccessor dbContextAccessor, IDependentRepository dependentRepository) : base(dbContextAccessor) 
        { 
            _dependentRepository = dependentRepository;
        }

        public async Task<CreateObjectResponse> Create(PayCheck value)
        {
            DataComtext.Add(value.ToEntity());

            return await Task.FromResult(new CreateObjectResponse((object)value.ID));
        }

        public async Task<IEnumerable<PayCheck>?> GetByEmployeeID(int employeeID)
        {
            var dependents = await _dependentRepository.GetByEmployeeID(employeeID);

            return await Task.FromResult(DataComtext.PayStatements
                        .Where(pay => pay.EmployeeID == employeeID)
                        .Select(s => s.ToModel()));
        }
    }
}
