using Api.Api.Utility;
using Api.BusinessLogic.Calculations;
using Api.BusinessLogic.Calculations.Interfaces;
using Api.BusinessLogic.Services.Interfaces;

namespace Api.BusinessLogic.Services
{
    public class CalculationLibraryFactory : ICalculationLibraryFactory
    {
        private readonly IBenefitsConfig _benefitsConfig;
        public CalculationLibraryFactory(IBenefitsConfig benefitsConfig)
        {
            _benefitsConfig = benefitsConfig;
        }
        public ICalculationsLibrary Create(int payCheckPeriod)
        {
            switch(payCheckPeriod)
            {
                case var exp when payCheckPeriod < _benefitsConfig.PAY_PERIODS_PER_YEAR:
                    return new StandardCalculationLibrary(_benefitsConfig);
                case var exp when payCheckPeriod == _benefitsConfig.PAY_PERIODS_PER_YEAR:
                    return new LastPayCheckOfYearCalculationLibrary(_benefitsConfig);
                default:
                    throw new NotImplementedException("Calculator Not Implemented");
            }
        }
    }
}
