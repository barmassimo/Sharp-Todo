using MB.SharpTodo.Core.Application;
using MB.SharpTodo.Core.Domain.Interfaces;
using MB.SharpTodo.Core.Domain.Services;
using Ninject.Modules;
using MB.SharpTodo.Core.DataAccess.Repositories;

namespace MB.SharpTodo.Core
{
    public class SharpTodoNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ISharpTodoService>().To<SharpTodoService>();

            Bind<ICategoryResolverService>().To<CategoryResolverService>();

            Bind<ITodoItemRepository>().To<TodoItemRepository>();
            Bind<ICategoryRepository>().To<CategoryRepository>();

            Bind<SharpTodoDbContext>().ToSelf();
        }
    }
}
