namespace Api.Data.Entities
{
    public enum DeductionTypes
    {
        BenefitsBase,
        DependentBenefitsFee,
        HighEarnerBenefitsFee,
        SeniorBenefitsFee
    }

    public class PayStatementEntity : EntityBase
    {
        public Guid ID { get; set; } = Guid.NewGuid();
        public int Order { get; set; }
        public int EmployeeID { get; set; }
        public decimal GrossPay { get; set; }
        public decimal NetPay { get; set; }
        public int NumberOfDependents { get; set; } 
        public Dictionary<DeductionTypes, decimal> Deductions { get; set; } = new Dictionary<DeductionTypes, decimal>();
    }
}
