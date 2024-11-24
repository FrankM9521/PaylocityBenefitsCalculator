using Api.BusinessLogic.Models.CalculatePayroll;

namespace Api.BusinessLogic.Models
{
    public class PayStatement
    {
        public PayStatement() 
        {
            PreviousPayrollStatements = new List<CalculatePayrollStatement>();
            Employee = new Employee();
        }
        public PayStatement(IEnumerable<CalculatePayrollStatement>? previousPayrollStatements, Employee employee) 
        { 
            PreviousPayrollStatements = previousPayrollStatements == null ? new List<CalculatePayrollStatement>() : previousPayrollStatements;
            Employee = employee;
        }

        public Guid ID { get; init; } = Guid.NewGuid();
        public int EmployeeID { get; set; }
        public Employee Employee { get; }
        public decimal GrossPay { get; init; }
        public int Order { get; set; }
        public decimal NetPay { get { return Math.Round(GrossPay - Deductions.Sum(d => d.Value), 2); } }
        public decimal TotalDeductions { get {  return Deductions.Values.Sum(d => d);  } }
        public Dictionary<DeductionTypes, decimal> Deductions { get; } = new Dictionary<DeductionTypes, decimal>();
        public IEnumerable<CalculatePayrollStatement> PreviousPayrollStatements { get;  }
        public int NumberOfDependents { get; set; }
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
