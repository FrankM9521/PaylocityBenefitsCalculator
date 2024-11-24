using Api.Api.Utility;
using Api.BusinessLogic.Calculations.Deductions;
using Api.BusinessLogic.Calculations.Interfaces;
using Api.BusinessLogic.Calculators;

namespace Api.BusinessLogic.Calculations
{
    public class DeductionCalculatorCollection : IDeductionCalculatorCollection
    {
        private readonly IEnumerable<ICalculate> _deductions;
        public DeductionCalculatorCollection(ICalculationsLibrary library, IBenefitsConfig benefitsConfig)
        {
            _deductions = new List<ICalculate>
            {
                new StandardBenfitDeductionCalculator(library),
               new DependentDeductionCalculator(library),
               new CalculateHighEarnerDeductionCalculator(library),
               new SeniorBenefitsDeductionCalculator(library, benefitsConfig)
            };
        }

        public IEnumerable<ICalculate> Deductions => _deductions;
    }
}
