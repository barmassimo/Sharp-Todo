using System;
using System.Data.Entity;
using System.Linq;
using MB.SharpTodo.Core.Domain.Interfaces;

namespace MB.SharpTodo.Core.DataAccess.Repositories
{
    public class GenericRepository<T> : 
        IDisposable,
        IGenericRepository<T> where T : class
    {
        private SharpTodoDbContext _context;

        public GenericRepository(SharpTodoDbContext context)
        {
            _context = context;
        }

        public virtual T FindById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        // public virtual IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        //{
        //    return DefaulQuery().Where(predicate);
        //}

        public virtual IQueryable<T> FindAll()
        {
            return DefaulQuery();
        }

        public virtual void Add(T item)
        {
            _context.Set<T>().Add(item);
        }

        public virtual void Remove(T item)
        {
            _context.Set<T>().Remove(item);
        }

        public virtual void SetModified(T item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }        

        public virtual void SaveChanges()
        {
            _context.SaveChanges();
        }

        protected virtual IQueryable<T> DefaulQuery()
        {
            return _context.Set<T>();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }

        ~GenericRepository()
        {
            Dispose(false);
        }
    }
}
