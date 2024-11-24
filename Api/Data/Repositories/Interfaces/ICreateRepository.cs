using Api.BusinessLogic.Models.Response;

namespace Api.Data.Repositories.Interfaces
{
    public interface ICreateRepository<T>
    {
        Task<CreateObjectResponse> Create(T value);
    }
}
