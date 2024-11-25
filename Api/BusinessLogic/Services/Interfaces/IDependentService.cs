using Api.BusinessLogic.Models;

namespace Api.BusinessLogic.Services.Interfaces
{
    public interface IDependentService : IAPIGet<Dependent, int>, IAPIPost<Dependent> { }
}
