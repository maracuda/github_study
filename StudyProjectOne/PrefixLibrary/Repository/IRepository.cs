namespace PrefixLibrary.Repository
{
    public interface IRepository<T>
    {
        void Add(T entity);
        void Delete(string id);
        void Update(string id, T entity);
        T[] ReadAll();
        T Read(string id);
    }
}