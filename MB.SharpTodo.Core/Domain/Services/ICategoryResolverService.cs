namespace MB.SharpTodo.Core.Domain.Services
{
    public interface ICategoryResolverService
    {
        Category FindOrCreateCategory(string description);
    }
}