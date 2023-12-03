﻿namespace MyLeaveManagement.Contracts
{
    public interface IReposBase<T>where T : class
    {
        Task<ICollection<T>> GetAll();
        Task<T> findByID(int id);

        Task<bool> Create(T entity);
        Task<bool> update(T entity);
        Task<bool> delete(T entity);
        Task<bool> save();
        Task<bool> isExists(int id);
    }
}
