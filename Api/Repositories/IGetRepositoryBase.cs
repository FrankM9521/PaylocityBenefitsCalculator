﻿namespace Api.Repositories
{
    public interface IGetRepositoryBase<T>
    {
        Task<IEnumerable<T>> Get();
        Task<T?> GetByID(int id);
    }
}
