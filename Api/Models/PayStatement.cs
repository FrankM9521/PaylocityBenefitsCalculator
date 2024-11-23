namespace Api.Models
{
    public enum DeductionTypes
    {
        BenefitsBase,
        DependentBenefitsFee,
        HighEarnerBenefitsFee,
        SeniorBenefitsFee
    }

    public class PayStatement
    {
        public Guid ID { get; init; } = Guid.NewGuid();
        public Employee Employee { get; init; }
        public decimal GrossPay { get; init; }
        public int Order { get; set; }
        public decimal NetPay 
        { 
            get
            {
                var deductions = Deductions.Sum(d => d.Value);
                return Math.Round(GrossPay - deductions, 2);   
            }
        }
        public decimal TotalDeductions
        {
            get
            {
                return Deductions.Values.Sum(d => d);
            }
        }

        public Dictionary<DeductionTypes, decimal> Deductions { get; } = new Dictionary<DeductionTypes, decimal>();
        public async Task AddDeduction(DeductionTypes deductionType, decimal amount)
        {
            // assuming 1 to 1 deduction type to value
            if (Deductions.ContainsKey(deductionType))
            {
                throw new Exception("Duplicate Deduction Type!"); 
            }

            Deductions.Add(deductionType, amount);
        }
    }
}
