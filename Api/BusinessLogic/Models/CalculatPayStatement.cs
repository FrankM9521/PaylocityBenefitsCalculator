using Api.BusinessLogic.Models.CalculatePayroll;

namespace Api.BusinessLogic.Models
{
    public class CalculatePayStatement
    {
        public CalculatePayStatement() 
        {
            PreviousPayrollStatements = new List<CalculatePayrollStatement>();
            Employee = new Employee();
        }
        public CalculatePayStatement(IEnumerable<CalculatePayrollStatement>? previousPayrollStatements, Employee employee) 
        { 
            PreviousPayrollStatements = previousPayrollStatements == null ? new List<CalculatePayrollStatement>() : previousPayrollStatements;
            Employee = employee;
            Order = previousPayrollStatements == null ? 1 :  previousPayrollStatements.Count() + 1;
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
