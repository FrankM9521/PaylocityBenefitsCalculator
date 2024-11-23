using Api.BusinessLogic.Models;

namespace Api.BusinessLogic.Calculations.Interfaces
{
    /*
     * Library of all Caclculations Used, easy to modify,
     */

    public interface ICalculationsLibrary
    {
        decimal GetSeniorDeduction(PayStatement payStatement);
        decimal GetHighEarnersDeduction(PayStatement payStatement);
        decimal GetDependentDeduction(PayStatement payStatement);
        decimal GetBaseDeduction(PayStatement payStatement);
    }
}
