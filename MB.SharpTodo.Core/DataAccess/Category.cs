using System.Data.Entity.ModelConfiguration;

namespace MB.SharpTodo.Core.Domain
{
    public partial class Category
    {
        internal class CategoryMapping: EntityTypeConfiguration<Category>
        {
            public CategoryMapping()
            {
                ToTable("Category", "dbo");
                HasKey(it => it.Id);
            }
        }
    }
}
