﻿using Paylocity.PayChecks.Models;
using Paylocity.PayChecks.Services.Interfaces;
using Paylocity.Shared.Config;

namespace Paylocity.PayChecks.Services.Calculators.CalculationLibraries
{
    /// <summary>
    /// Calculates the last paycheck by looking at what was distributed in the previous checks
    /// and subtracting that from the yearly amount
    /// </summary>
    public class LastPayCheckOfYearCalculationLibrary : ILastPayCheckOfYearCalculationLibrary
    {
        private IBenefitsConfig _benefitsConfig;
        public LastPayCheckOfYearCalculationLibrary(IBenefitsConfig benefitsConfig)
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
            var yearlyDependentDeduction = _benefitsConfig.DEPENDENT_BENFITS_MONTHLY_DEDUCTION_AMT * 12 * payStatement.NumberOfDependents;
            var currentDependentDeduction = payStatement.PreviousPayChecks.Sum(s => s.Deductions[DeductionTypes.DependentBenefitsFee]);

            return yearlyDependentDeduction - currentDependentDeduction;
        }

        public decimal GetHighEarnersDeduction(CalculatePayCheck payStatement)
        {
            var deduction = 0M;

            if (payStatement.Salary > _benefitsConfig.HIGH_EARNER_BENEFITS_YEARLY_DEDUCTION_FLOOR_AMT)
            {
                var yearlyHighEarnerDeduction = Math.Round(payStatement.Salary * _benefitsConfig.HIGH_EARNERS_BENEFITS_YEARLY_DEDUCTION_PCT, 2);
                var currentHighEarnerDeduction = payStatement.PreviousPayChecks.Sum(s => s.Deductions[DeductionTypes.HighEarnerBenefitsFee]);

                deduction = yearlyHighEarnerDeduction - currentHighEarnerDeduction;
            }

            return deduction;
        }

        public decimal GetSeniorDeduction(CalculatePayCheck payStatement)
        {
            var yearlySeniorDeduction = _benefitsConfig.SENIOR_BENEFITS_MONTHLY_DEDUCTION_AMT * 12;
            var currentHighEarnersDeduction = payStatement.PreviousPayChecks.Sum(s => s.Deductions[DeductionTypes.SeniorBenefitsFee]);

            return yearlySeniorDeduction - currentHighEarnersDeduction;
        }
    }
}
