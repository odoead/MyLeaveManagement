namespace MyLeaveManagement.Contracts
{
    public interface IGenericRepositoryBase<T> where T:class 
    {

        ICollection<T> GetAll();
        T findByID(int id);

        Task <bool> CreateAsync(T entity);
        Task<bool> update(T entity);
        Task<bool> delete(T entity);
        Task<bool> save();
        Task<bool> isExists(int id);
    }
}
