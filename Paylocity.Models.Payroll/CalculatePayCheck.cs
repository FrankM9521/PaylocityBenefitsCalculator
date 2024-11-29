namespace Paylocity.PayChecks.Models
{
    public class CalculatePayCheck
    {
        public CalculatePayCheck(IEnumerable<PayCheck>? previousPayrollStatements, decimal salary) //, Employee employee)
        {
            PreviousPayChecks = previousPayrollStatements ?? new List<PayCheck>();
            Order = previousPayrollStatements == null ? 1 : previousPayrollStatements.Count() + 1;
            Salary = salary;
        }
        public Guid ID { get; init; } = Guid.NewGuid();
        public decimal GrossPay { get; init; }
        public decimal Salary { get; }
        public int Order { get; set; }
        public decimal NetPay { get { return Math.Round(GrossPay - Deductions.Sum(d => d.Value), 2); } }
        public decimal TotalDeductions { get { return Deductions.Values.Sum(d => d); } }
        public int NumberOfDependents {  get; set; }    
        public int EmployeeID { get; set; }
        public Dictionary<DeductionTypes, decimal> Deductions { get; } = new Dictionary<DeductionTypes, decimal>();
        public IEnumerable<PayCheck> PreviousPayChecks { get; }
        
        //DOB hacky, lazy
        public DateTime EmployeeDateOfBirth { get; set; }   

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
