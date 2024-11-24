using Api.Api.Utility;
using Api.BusinessLogic.Calculations.Interfaces;
using Api.BusinessLogic.Models;
using Api.Data;

namespace Api.BusinessLogic.Calculations
{
    public class StandardCalculationLibrary : ICalculationsLibrary
    {
        private readonly IBenefitsConfig _benefitsConfig;

        public StandardCalculationLibrary(IBenefitsConfig benefitsConfig)
        {
            _benefitsConfig = benefitsConfig;
        }   

        public decimal GetHighEarnersDeduction(CalculatePayStatement payStatement)
        {
            var yearlyGrossPay = GetEstimatedGrossYearlyPay(payStatement);
            var deduction = 0M;

            if (yearlyGrossPay > _benefitsConfig.HIGH_EARNER_BENEFITS_YEARLY_DEDUCTION_FLOOR_AMT)
            {
                deduction = Math.Round(yearlyGrossPay * _benefitsConfig.HIGH_EARNERS_BENEFITS_YEARLY_DEDUCTION_PCT / _benefitsConfig.PAY_PERIODS_PER_YEAR, 2);

                if (deduction > payStatement.NetPay)
                {
                    deduction = payStatement.NetPay;
                }
            }

            return deduction;
        }

        public decimal GetDependentDeduction(CalculatePayStatement payStatement)
        {
            var numberOfDependents = payStatement.Employee.Dependents.Count();
            var deduction = Math.Round(numberOfDependents * _benefitsConfig.DEPENDENT_BENFITS_MONTHLY_DEDUCTION_AMT * 12 / _benefitsConfig.PAY_PERIODS_PER_YEAR, 2);

            if (deduction > payStatement.NetPay)
            {
                deduction = payStatement.NetPay;
            }

            return deduction;
        }

        public decimal GetBaseDeduction(CalculatePayStatement payStatement)
        {
            return Math.Round(_benefitsConfig.EMPLOYEE_BENEFITS_BASE_MONTHLY_DEDUCTION_AMT * 12 / _benefitsConfig.PAY_PERIODS_PER_YEAR, 2);
        }

        public decimal GetSeniorDeduction(CalculatePayStatement payStatement)
        {
            return Math.Round(_benefitsConfig.SENIOR_BENEFITS_MONTHLY_DEDUCTION_AMT * 12 / _benefitsConfig.PAY_PERIODS_PER_YEAR, 2);
        }

        private decimal GetEstimatedGrossYearlyPay(CalculatePayStatement payStatement)
        {
            var estGrossPay = 0M;
       
            if (payStatement.PreviousPayrollStatements.Count() > 0)
            {
                var ytdGrossPay = payStatement.PreviousPayrollStatements.Sum(pay => pay.GrossPay) + payStatement.GrossPay;

                estGrossPay = Math.Round(ytdGrossPay / (payStatement.PreviousPayrollStatements.Count() + 1) * _benefitsConfig.PAY_PERIODS_PER_YEAR, 2);
            }
            else
            {
                estGrossPay = payStatement.GrossPay * _benefitsConfig.PAY_PERIODS_PER_YEAR;
            }

            return estGrossPay;
        }
    }
}
