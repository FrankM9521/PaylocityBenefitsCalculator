using Api.BusinessLogic.Mappers;
using Api.BusinessLogic.Models;
using Api.BusinessLogic.Models.Response;
using Api.Data.Repositories.Interfaces;

namespace Api.Data.Repositories
{
    public interface IPayStatementRepository : ICreateRepository<PayStatement> {  }

    public class PayStatementRepository : RepositoryBase,  IPayStatementRepository
    {
        private readonly IDependentRepository _dependentRepository;
        public PayStatementRepository(IDbContextAccessor dbContextAccessor, IDependentRepository dependentRepository) : base(dbContextAccessor) 
        { 
            _dependentRepository = dependentRepository;
        }

        public async Task<CreateObjectResponse> Create(PayStatement value)
        {
            DataComtext.Add(value.ToEntity());

            return await Task.FromResult(new CreateObjectResponse((object)value.ID));
        }

        public async Task<IEnumerable<PayStatement>?> GetByEmployeeID(int employeeID)
        {
            var dependents = await _dependentRepository.GetByEmployeeID(employeeID);

            return await Task.FromResult(DataComtext.PayStatements
                        .Where(pay => pay.EmployeeID == employeeID)
                        .Select(s => s.ToModel()));
        }
    }
}
