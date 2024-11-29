namespace Paylocity.Shared.Services.Interfaces
{
    public interface IAPIGet<TType, TKey>
    {
        Task<IEnumerable<TType>> GetAll();
        Task<TType?> GetByID(TKey id);
    }
}
