using Paylocity.PayChecks.Entities;
using Paylocity.Shared.Entities;

namespace Paylocity.PayChecks.DataContext
{
    /*
     * Fake DB, Created as a singleton instance
     */
    public sealed class PayChecksDataContext
    {
        private static readonly Lazy<PayChecksDataContext> lazy = new Lazy<PayChecksDataContext>(() => new PayChecksDataContext());
        // since most lookups will be by employee, I am using employee id as the key for faster lookups

        private static List<PayCheckEntity> _payStatements;
        private static List<DeductionEntity> _deductions;
        public static PayChecksDataContext Instance => lazy.Value;

        public async Task Add<T>(T entity) where T : EntityBase
        {
            switch (entity)
            {
                case PayCheckEntity payStatement:
                    _payStatements.Add(payStatement);
                    break;
                case DeductionEntity deduction:
                    _deductions.Add(deduction);
                    break;
                default:
                    throw new Exception("Unsupported Type");
            }
        }

        public void ClearPayChecks()
        {
            if (_payStatements != null)
            {
                _payStatements.Clear();
            }
        }

        public IEnumerable<PayCheckEntity> PayStatements
        {
            get { return _payStatements ?? (_payStatements = new List<PayCheckEntity>()); }
        }

        public IEnumerable<DeductionEntity> Deductions
        {
            get { return _deductions ?? (_deductions = new List<DeductionEntity>()); }
        }
    }
}
