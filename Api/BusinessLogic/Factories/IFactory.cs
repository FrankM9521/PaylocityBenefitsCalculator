namespace Api.BusinessLogic.Factories
{
    public interface IFactory<TOut, TIn>
    {
        Task<TOut> Create(TIn request);
    }
}
