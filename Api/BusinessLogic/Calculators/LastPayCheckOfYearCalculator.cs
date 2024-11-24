using Api.Api.Utility;
using Api.BusinessLogic.Calculations.Interfaces;
using Api.BusinessLogic.Models;

namespace Api.BusinessLogic.Calculations
{
    public class LastPayCheckOfYearCalculator : ICalculationsLibrary
    {
        private IBenefitsConfig _benefitsConfig;
        public LastPayCheckOfYearCalculator(IBenefitsConfig benefitsConfig) 
        { 
            _benefitsConfig = benefitsConfig;
        }

        public decimal GetBaseDeduction(CalculatePayCheck payStatement)
        {
            var yearlyBaseDeduction = _benefitsConfig.BASE_BENEFITS_MONTHLY_DEDUCTION_AMT * 12;
            var currentBaseDeductionBalance = payStatement.PreviousPayChecks.Sum(d => d.Deductions[DeductionTypes.BenefitsBase]);

            return yearlyBaseDeduction - currentBaseDeductionBalance;
        }

        public decimal GetDependentDeduction(CalculatePayCheck payStatement)
        {
            var yearlyDependentDeduction = _benefitsConfig.DEPENDENT_BENFITS_MONTHLY_DEDUCTION_AMT * 12;
            var currentDependentDeduction = payStatement.PreviousPayChecks.Sum(s => s.Deductions[DeductionTypes.DependentBenefitsFee]);

            return yearlyDependentDeduction - currentDependentDeduction;
        }

        public decimal GetHighEarnersDeduction(CalculatePayCheck payStatement)
        {
            var yearlyHighEarnerDeduction = Math.Round(payStatement.Salary * _benefitsConfig.HIGH_EARNERS_BENEFITS_YEARLY_DEDUCTION_PCT, 2);
            var currentHighEarnerDeduction = payStatement.PreviousPayChecks.Sum(s => s.Deductions[DeductionTypes.HighEarnerBenefitsFee]);

            return yearlyHighEarnerDeduction - currentHighEarnerDeduction;
        }

        public decimal GetSeniorDeduction(CalculatePayCheck payStatement)
        {
            var yearlySeniorDeduction = _benefitsConfig.SENIOR_BENEFITS_MONTHLY_DEDUCTION_AMT * 12;
            var currentHighEarnersDeduction = payStatement.PreviousPayChecks.Sum(s => s.Deductions[DeductionTypes.SeniorBenefitsFee]);

            return yearlySeniorDeduction - currentHighEarnersDeduction;
        }
    }
}
