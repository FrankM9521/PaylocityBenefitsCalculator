using Api.BusinessLogic.Calculations.Interfaces;
using Api.BusinessLogic.Models;
using Api.Data;

namespace Api.BusinessLogic.Calculations
{
    public class StandardCalculationLibrary : ICalculationsLibrary
    {
        public decimal GetHighEarnersDeduction(PayStatement payStatement)
        {
            var yearlyGrossPay = GetEstimatedGrossYearlyPay(payStatement);
            var deduction = 0M;

            if (yearlyGrossPay > Constants.HIGH_EARNER_BENEFITS_YEARLY_DEDUCTION_FLOOR_AMT)
            {
                deduction = Math.Round(yearlyGrossPay * Constants.HIGH_EARNERS_BENEFITS_YEARLY_DEDUCTION_PCT / Constants.PAY_PERIODS_PER_YEAR, 2);

                if (deduction > payStatement.NetPay)
                {
                    deduction = payStatement.NetPay;
                }
            }

            return deduction;
        }

        public decimal GetDependentDeduction(PayStatement payStatement)
        {
            var numberOfDependents = payStatement.Employee.Dependents.Count();
            var deduction = Math.Round(numberOfDependents * Constants.DEPENDENT_BENFITS_MONTHLY_DEDUCTION_AMT * 12 / Constants.PAY_PERIODS_PER_YEAR, 2);

            if (deduction > payStatement.NetPay)
            {
                deduction = payStatement.NetPay;
            }

            return deduction;
        }

        public decimal GetBaseDeduction(PayStatement payStatement)
        {
            return Math.Round(Constants.EMPLOYEE_BENEFITS_BASE_MONTHLY_DEDUCTION_AMT * 12 / Constants.PAY_PERIODS_PER_YEAR, 2);
        }

        public decimal GetSeniorDeduction(PayStatement payStatement)
        {
            return Math.Round(Constants.SENIOR_BENEFITS_MONTHLY_DEDUCTION_AMT * 12 / Constants.PAY_PERIODS_PER_YEAR, 2);
        }

        private decimal GetEstimatedGrossYearlyPay(PayStatement payStatement)
        {
            var estGrossPay = 0M;
            var currentStatements = DB.PayStatements.Where(pay => pay.EmployeeID == payStatement.Employee.Id);

            // if they have been paid prior, we need to average out based on how many previous pay statements (they may have gottebn a raise)
            if (currentStatements.Any())
            {
                var ytdGrossPay = currentStatements.Sum(pay => pay.GrossPay) + payStatement.GrossPay;

                estGrossPay = Math.Round(ytdGrossPay / (currentStatements.Count() + 1) * Constants.PAY_PERIODS_PER_YEAR, 2);
            }
            else
            {
                estGrossPay = payStatement.GrossPay * Constants.PAY_PERIODS_PER_YEAR;
            }

            return estGrossPay;
        }

        //
        private DataBase DB
        {
            get { return DataBase.Instance; }
        }
    }
}
