using Api.BusinessLogic.Models;
using Api.BusinessLogic.Models.Response;

namespace Api.BusinessLogic.Services.Interfaces
{
    public interface IAPIPost<T>
    {
        Task<CreateObjectResponse> Create(Dependent newDependent);
    }
}
