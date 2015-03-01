using System.Data.Entity.ModelConfiguration;

namespace MB.SharpTodo.Core.Domain
{
    public partial class TodoComment
    {
        internal class TodoCommentMapping : EntityTypeConfiguration<TodoComment>
        {
            public TodoCommentMapping()
            {
                ToTable("TodoComment", "dbo");
                HasKey(it => it.Id);
            }
        }
    }
}


