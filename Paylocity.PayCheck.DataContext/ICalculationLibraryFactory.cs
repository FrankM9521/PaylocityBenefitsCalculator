namespace Paylocity.PayChecks.Services.Interfaces
{
    public interface ICalculationLibraryFactory
    {
        public ICalculationsLibrary Create(int payCheckPeriod);
    }
}
