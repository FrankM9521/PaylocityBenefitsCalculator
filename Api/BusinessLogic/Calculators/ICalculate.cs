using Api.BusinessLogic.Models;

namespace Api.BusinessLogic.Calculators
{
    public interface ICalculate
    {
        Task<CalculatePayStatement> Calculate(CalculatePayStatement payStatement);
    }
}
