using Api.BusinessLogic.Models;

namespace Api.BusinessLogic.Calculators
{
    public interface ICalculate
    {
        Task<CalculatePayCheck> Calculate(CalculatePayCheck payStatement);
    }
}
