using Api.BusinessLogic.Models;

namespace Api.BusinessLogic.Calculations.Interfaces
{
    /*
     * Library of all Caclculations Used, easy to modify,
     */

    public interface ICalculationsLibrary
    {
        decimal GetSeniorDeduction(CalculatePayStatement payStatement);
        decimal GetHighEarnersDeduction(CalculatePayStatement payStatement);
        decimal GetDependentDeduction(CalculatePayStatement payStatement);
        decimal GetBaseDeduction(CalculatePayStatement payStatement);
    }
}
