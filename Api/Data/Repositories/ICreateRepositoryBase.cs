using Api.BusinessLogic.Models.Response;

namespace Api.Data.Repositories
{
    public interface ICreateRepositoryBase<T>
    {
        Task<CreateResponse> Create(T value);
    }
}
