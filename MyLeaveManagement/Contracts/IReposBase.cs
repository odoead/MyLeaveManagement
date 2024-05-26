namespace MyLeaveManagement.Contracts
{
    public interface IReposBase<T>
        where T : class
    {
        Task<ICollection<T>> GetAllAsync();
        Task<T> FindByIdAsync(int id);
        Task<bool> CreateAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
        Task<bool> SaveAsync();
        Task<bool> IsExistsAsync(int id);
    }
}
