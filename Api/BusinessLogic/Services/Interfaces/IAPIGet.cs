namespace Api.BusinessLogic.Services.Interfaces
{
    public interface IAPIGet<TType, TKey>
    {
        Task<IReadOnlyCollection<TType>> GetAll();
        Task<TType?> GetByID(TKey id);
    }
}
