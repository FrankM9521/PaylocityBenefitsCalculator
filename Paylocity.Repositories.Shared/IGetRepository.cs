namespace Paylocity.Shared.Repositories.Interfaces
{
    public interface IGetRepository<T>
    {
        Task<IEnumerable<T>> Get();
        Task<T?> GetByID(int id);
    }
}
