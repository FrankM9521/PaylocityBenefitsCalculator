using Paylocity.Employees.Models;
using Paylocity.Shared.Services.Interfaces;

namespace Paylocity.Employees.Services.Interfaces
{
    public interface IDependentService : IAPIGet<Dependent, int>, IAPIPost<Dependent> { }
}
