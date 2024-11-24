using Api.BusinessLogic.Models;
using Api.BusinessLogic.Services.Interfaces;

namespace Api.BusinessLogic.Services
{
    public interface IDependentService : IAPIGet<Dependent, int>, IAPIPost<Dependent> { }
}
