using MB.SharpTodo.Core.Domain;
using MB.SharpTodo.Core.Domain.Interfaces;

namespace MB.SharpTodo.Core.DataAccess.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(SharpTodoDbContext context): base(context) {}
    }
}
