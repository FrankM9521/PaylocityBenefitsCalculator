using Api.BusinessLogic.Models;

namespace Api.BusinessLogic.Calculators
{
    public interface ICalculate
    {
        Task<PayStatement> Calculate(PayStatement payStatement);
    }
}
