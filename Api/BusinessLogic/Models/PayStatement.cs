namespace Api.BusinessLogic.Models
{
    public class PayStatement
    {
        public Guid ID { get; init; } = Guid.NewGuid();
        public Employee Employee { get; init; }
        public decimal GrossPay { get; init; }
        public int Order { get; set; }
        public decimal NetPay { get { return Math.Round(GrossPay - Deductions.Sum(d => d.Value), 2); } }
        public decimal TotalDeductions { get {  return Deductions.Values.Sum(d => d);  } }
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
