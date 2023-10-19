namespace MyLeaveManagement.Contracts
{
    public interface IReposBase<T>where T : class
    {
        ICollection<T> GetAll();
        T findByID(int id);

        bool Create(T entity);
        bool update(T entity);
        bool delete(T entity);
        bool save();
        bool isExists(int id);
    }
}
