using Paylocity.PayChecks.Models;

namespace Paylocity.PayChecks.Services.Interfaces
{
    /*
     * Library of all Caclculations Used, easy to modify,
     */

    public interface ICalculationsLibrary
    {
        decimal GetSeniorDeduction(CalculatePayCheck payStatement);
        decimal GetHighEarnersDeduction(CalculatePayCheck payStatement);
        decimal GetDependentDeduction(CalculatePayCheck payStatement);
        decimal GetBaseDeduction(CalculatePayCheck payStatement);
    }
}
