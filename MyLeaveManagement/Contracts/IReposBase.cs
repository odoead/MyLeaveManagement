namespace MyLeaveManagement.Contracts
{
    public interface IReposBase<T>where T : class
    {
        Task<ICollection<T>> GetAllAsync();
       Task< T> findByIDAsync(int id);

        Task<bool> CreateAsync(T entity);
        Task<bool> updateAsync(T entity);
        Task<bool> deleteAsync(T entity);
        Task<bool> saveAsync();
        Task<bool> isExistsAsync(int id);
    }
}
