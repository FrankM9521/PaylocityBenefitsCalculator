using Paylocity.Shared.Entities;

namespace Paylocity.PayChecks.Entities
{
    public class DeductionEntity : EntityBase
    {
        public Guid ID { get; set; } = Guid.NewGuid();
        public Guid PaycheckID { get; set; }
        public DeductionTypes DeductionType { get; set; }
        public decimal DeductionAmount { get; set; }
    }
}
