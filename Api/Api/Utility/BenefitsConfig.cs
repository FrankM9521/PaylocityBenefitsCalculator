namespace Api.Api.Utility
{
    public interface IBenefitsConfig
    {
        int PAY_PERIODS_PER_YEAR { get; set; }
        decimal BASE_BENEFITS_MONTHLY_DEDUCTION_AMT { get; set; }
        decimal DEPENDENT_BENFITS_MONTHLY_DEDUCTION_AMT { get; set; }
        decimal HIGH_EARNER_BENEFITS_YEARLY_DEDUCTION_FLOOR_AMT { get; set; }
        decimal HIGH_EARNERS_BENEFITS_YEARLY_DEDUCTION_PCT { get; set; }
        decimal SENIOR_BENEFITS_MONTHLY_DEDUCTION_AMT { get; set; }
        int SENIOR_BENFITS_AGE_FLOOR { get; set; }
    }

    /// <summary>
    /// This would normally come from the DB and be injected, time constraints
    /// </summary>
    public class BenefitsConfig : IBenefitsConfig
    {
        public int PAY_PERIODS_PER_YEAR { get; set; } = 26;
        public decimal BASE_BENEFITS_MONTHLY_DEDUCTION_AMT { get; set; } = 1000M;
        public decimal DEPENDENT_BENFITS_MONTHLY_DEDUCTION_AMT { get; set; } = 600M;
        public decimal HIGH_EARNER_BENEFITS_YEARLY_DEDUCTION_FLOOR_AMT { get; set; } = 80000M;
        public decimal HIGH_EARNERS_BENEFITS_YEARLY_DEDUCTION_PCT { get; set; } = .02M;
        public decimal SENIOR_BENEFITS_MONTHLY_DEDUCTION_AMT { get; set; } = 200M;
        public int SENIOR_BENFITS_AGE_FLOOR { get; set; } = 51;
    }
}
