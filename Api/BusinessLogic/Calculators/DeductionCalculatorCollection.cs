using Api.BusinessLogic.Calculations.Deductions;
using Api.BusinessLogic.Calculations.Interfaces;

namespace Api.BusinessLogic.Calculations
{
    public class DeductionCalculatorCollection : IDeductionCalculatorCollection
    {
        private readonly IEnumerable<IDeduction> _deductions;
        public DeductionCalculatorCollection(ICalculationsLibrary library)
        {
            _deductions = new List<IDeduction>
            {
                new StandardBenfitDeductionCalculator(library),
               new DependentDeductionCalculator(library),
               new CalculateHighEarnerDeductionCalculator(library),
               new SeniorBenefitsDeductionCalculator(library)
            };
        }

        public IEnumerable<IDeduction> Deductions => _deductions;
    }
}
