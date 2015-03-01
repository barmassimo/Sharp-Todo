using MB.SharpTodo.Core.Domain;
using System.Data.Entity;

namespace MB.SharpTodo.Core.DataAccess.Repositories
{
    public class SharpTodoDbContext : DbContext
    {
        // public DbSet<TodoItem> TodoItemSet { get; set; }

        public SharpTodoDbContext()
            : base("name=SharpTodoDefaultConnection")
        {
            // TodoItemSet = Set<TodoItem>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            IDatabaseInitializer<SharpTodoDbContext> dbInitializer = null;
            Database.SetInitializer(dbInitializer);

            modelBuilder.Configurations.Add(new TodoItem.TodoItemMapping());
            modelBuilder.Configurations.Add(new TodoComment.TodoCommentMapping());
            modelBuilder.Configurations.Add(new Category.CategoryMapping());

            modelBuilder.Entity<TodoComment>()
             .HasRequired(c => c.TodoItem)
             .WithMany(t => t.Comments)//.Map(x => x.MapKey("TodoItem_Id"))
             .WillCascadeOnDelete(true);

            base.OnModelCreating(modelBuilder);
        }

        
    }
}
