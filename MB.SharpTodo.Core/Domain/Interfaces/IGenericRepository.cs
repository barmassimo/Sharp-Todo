using System.Linq;

namespace MB.SharpTodo.Core.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        T FindById(int id);
        IQueryable<T> FindAll();

        void Add(T item);
        void Remove(T item);

        void SetModified(T item);

        void SaveChanges();
        void Dispose();
    }
}
