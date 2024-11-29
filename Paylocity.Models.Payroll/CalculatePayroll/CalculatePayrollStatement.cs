namespace Paylocity.PayChecks.Models.CalculatePayroll
{
    public record CalculatePayrollStatement(Guid ID, int Order, decimal GrossPay, decimal NetPay, int NumberOfDependents, Dictionary<DeductionTypes, decimal> Deductions);
}
