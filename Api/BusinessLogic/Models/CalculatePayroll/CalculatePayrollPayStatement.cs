namespace Api.BusinessLogic.Models.CalculatePayroll
{
    public record CalculatePayrollPayStatement(Guid ID, int Order, decimal GrossPay, decimal NetPay, int NumberOfDependents, Dictionary<DeductionTypes, decimal> Deductions);
}
