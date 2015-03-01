using System.Data.Entity;
using System.Linq;
using MB.SharpTodo.Core.Domain;
using MB.SharpTodo.Core.Domain.Interfaces;

namespace MB.SharpTodo.Core.DataAccess.Repositories
{
    public class TodoItemRepository : GenericRepository<TodoItem>, ITodoItemRepository
    {
        public TodoItemRepository(SharpTodoDbContext context) : base(context) { }

        protected override IQueryable<TodoItem> DefaulQuery()
        {
            return base.DefaulQuery().Include(it => it.Category);
        }
    }
}
