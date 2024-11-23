namespace Api
{
    /// <summary>
    /// This would normally come from the DB and be injected, time constraints
    /// </summary>
    public class Constants
    {
        public const int PAY_PERIODS_PER_YEAR = 26;
        public const decimal PAY_PERIODS_PER_YEAR_AS_PCT = 0.26M;
        public const decimal EMPLOYEE_BENEFITS_BASE_MONTHLY_DEDUCTION_AMT = 1000M;
        public const decimal DEPENDENT_BENFITS_MONTHLY_DEDUCTION_AMT = 600M;
        public const decimal HIGH_EARNER_BENEFITS_YEARLY_DEDUCTION_FLOOR_AMT = 80000M;
        public const decimal HIGH_EARNERS_BENEFITS_YEARLY_DEDUCTION_PCT = .02M;
        public const decimal SENIOR_BENEFITS_MONTHLY_DEDUCTION_AMT = 200M;
        public const int SENIOR_BENFITS_AGE_FLOOR = 51;
    }
}
