using Api.Calculations.Deductions;

namespace Api.Calculations
{
    public interface IDeductionCalculatorCollection
    {
        IEnumerable<IDeduction> Deductions { get; }
    }

    /*  
     *  Collection of the various calculators used for deductions
     *  
     *  Currently just an unordered list, but could be ordered by priority or spme other criteria
     */ 
    public class DeductionCalculatorCollection : IDeductionCalculatorCollection
    {
        private readonly IEnumerable<IDeduction> _deductions;
        public DeductionCalculatorCollection()
        {
            _deductions = new List<IDeduction>
            {
                new CalculateStandardBenfitDeduction(),
               new CalculateDependentDeductions(),
               new CalculateHighEarnerDeduction(),
               new CalculateSeniorBenefitsDeduction()
            };
        }

        public IEnumerable<IDeduction> Deductions => _deductions;
    }
}
