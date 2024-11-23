using Api.Models;
using Api.Repositories;

namespace Api.Services
{
    public interface IDependentService : IAPIGet<Dependent>, IAPIPost<Dependent> {   }

    public class DependentService : IDependentService
    {
        private readonly IDependentRepository _dependentRepository;

        public DependentService(IDependentRepository dependentRepository)
        {
            _dependentRepository = dependentRepository;
        }
        public async Task<IReadOnlyCollection<Dependent>> GetAll()
        {
            var result = await _dependentRepository.Get();

            return result.ToList().AsReadOnly();
        }

        public async Task<Dependent?> GetByID(int id)
        {
            var result = await _dependentRepository.GetByID(id);

            return result;
        }

        public async Task<CreateResponse> Post(Dependent newDependent)
        {
            // rules are being checked at the repository layer, they really should be injected, but time constraints
            return await _dependentRepository.Create(newDependent);   
        }
    }
}
