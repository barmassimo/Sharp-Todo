using System.Data.Entity.ModelConfiguration;

namespace MB.SharpTodo.Core.Domain
{
    public partial class TodoItem
    {
        internal class TodoItemMapping: EntityTypeConfiguration<TodoItem>
        {
            public TodoItemMapping()
            {
                ToTable("TodoItem", "dbo");
                HasKey(it => it.Id);
            }
        }
    }
}
