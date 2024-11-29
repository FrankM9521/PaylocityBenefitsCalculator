using Paylocity.Shared.Models;

namespace Paylocity.Shared.Repositories.Interfaces
{
    public interface ICreateRepository<T>
    {
        Task<CreateObjectResponse> Create(T value);
    }
}
