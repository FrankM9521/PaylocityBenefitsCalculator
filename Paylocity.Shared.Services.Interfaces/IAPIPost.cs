using Paylocity.Employees.Models;
using Paylocity.Shared.Models;

namespace Paylocity.Shared.Services.Interfaces
{
    public interface IAPIPost<T>
    {
        Task<CreateObjectResponse> Create(Dependent newDependent);
    }
}
