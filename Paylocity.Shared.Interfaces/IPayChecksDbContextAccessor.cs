using Paylocity.Shared.Entities;
using Paylocity.PayChecks.Entities;

namespace Paylocity.PayChecks.DataContext.Interfaces
{
    public interface IPayChecksDbContextAccessor
    {
        //public DataBase DataBase { get; }
        List<PayCheckEntity> PayChecks { get; }
        void ClearPaychecks();
        Task Add<T>(T model) where T : EntityBase;
    }
}
