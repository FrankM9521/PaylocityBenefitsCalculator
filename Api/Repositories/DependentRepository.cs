using Api.Mappers;
using Api.Models;
using Api.Validation;
using System.Net;

namespace Api.Repositories
{
    public interface IDependentRepository : IGetRepositoryBase<Dependent>, ICreateRepositoryBase<Dependent> { }
    public class DependentRepository : IDependentRepository
    {
        private readonly ValidatiorCollection<Dependent> _validatiorCollection;

        public DependentRepository()
        {
            // Normally would want to inject the validators - time constraints
            _validatiorCollection = new ValidatiorCollection<Dependent>();

            _validatiorCollection.Add(new ValidateDependentOnlyHasOneSpouseOrDomesticPartner());
        }

        public async Task<IEnumerable<Dependent>> Get()
        {
            return await Task.FromResult(DB.Dependents.Select(d => d.ToDomain()).AsEnumerable());
        }

        public async Task<Dependent?> GetByID(int id)
        {
            var entity = DB.Dependents.FirstOrDefault(emp => emp.Id == id);

            return entity != null
                ? await Task.FromResult(entity.ToDomain())
                : null;
        }

        public async Task<CreateResponse> Create(Dependent newDependent)
        {
            var result = await  _validatiorCollection.Validate(newDependent);

            if (result.Success)
            {
                // next id
                newDependent.Id = DB.Dependents.Max(d => d.Id) + 1;

                DB.Add(newDependent.ToEntity());

                return new CreateResponse((int?) newDependent.Id);
            }
            else
            {
                return new CreateResponse((int?)null, false, HttpStatusCode.BadRequest, result.ErrorMessage);
            }
        }



        private DataBase DB
        {
            get { return DataBase.Instance; }
        }
    }
}
