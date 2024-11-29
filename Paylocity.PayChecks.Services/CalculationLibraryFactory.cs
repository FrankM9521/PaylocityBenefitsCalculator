using Microsoft.Extensions.DependencyInjection;
using Paylocity.PayChecks.Services.Interfaces;
using Paylocity.Shared.Config;

namespace Paylocity.PayChecks.Services
{
    /// <summary>
    /// Returns a standard calculator, unless it is the final period 
    /// </summary>
    public class CalculationLibraryFactory : ICalculationLibraryFactory
    {
        private readonly IBenefitsConfig _benefitsConfig;
        private readonly IServiceProvider _serviceProvider;
        public CalculationLibraryFactory(IBenefitsConfig benefitsConfig, IServiceProvider serviceProvider)
        {
            _benefitsConfig = benefitsConfig;
            _serviceProvider = serviceProvider;
        }
        public ICalculationsLibrary Create(int payCheckPeriod)
        {
            switch (payCheckPeriod)
            {
                case var exp when payCheckPeriod < _benefitsConfig.PAY_PERIODS_PER_YEAR:
                    return _serviceProvider.GetRequiredService<IStandardCalculationLibrary>();
                case var exp when payCheckPeriod == _benefitsConfig.PAY_PERIODS_PER_YEAR:
                    return  _serviceProvider.GetRequiredService<ILastPayCheckOfYearCalculationLibrary>();
                default:
                    throw new NotImplementedException("Calculator Not Implemented");
            }
        }
    }
}
