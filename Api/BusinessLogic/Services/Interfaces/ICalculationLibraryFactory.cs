using Api.BusinessLogic.Calculations.Interfaces;

namespace Api.BusinessLogic.Services.Interfaces
{
    public interface ICalculationLibraryFactory
    {
        public ICalculationsLibrary Create(int payCheckPeriod);
    }
}
