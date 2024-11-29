using Paylocity.PayChecks.DataContext.Interfaces;
using Paylocity.PayChecks.Entities;
using Paylocity.Shared.Entities;

namespace Paylocity.PayChecks.DataContext
{
    public class PayChecksDbContextAccessor : IPayChecksDbContextAccessor
    {
        public List<PayCheckEntity> PayChecks { get => DataBase.PayStatements.ToList(); }
        public List<DeductionEntity> Deductions { get => DataBase.Deductions.ToList(); }
        public void ClearPaychecks()
        {
            PayChecks?.Clear();
        }

        public async Task Add<T>(T entity) where T : EntityBase
        {
            await DataBase.Add(entity);
        }

        private PayChecksDataContext DataBase
        {
            get
            {
                return PayChecksDataContext.Instance;
            }
        }
    }
}
