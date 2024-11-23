using Api.Models;

namespace Api.Repositories
{
    public interface ICreateRepositoryBase<T>
    {
        Task<CreateResponse> Create(T value);
    }
}
