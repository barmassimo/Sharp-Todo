using System;
using System.Linq;
using MB.SharpTodo.Core.Domain;
using MB.SharpTodo.Core.Domain.Interfaces;
using MB.SharpTodo.Core.Domain.Services;

namespace MB.SharpTodo.Core.Application
{
    public class SharpTodoService: ISharpTodoService
    {
        private const int PriorotyMaxHot = 5;
        private const int HotDays = 7;

        private ITodoItemRepository _todoItemRepository { get; set; }
        private ICategoryRepository _categoryRepository { get; set; }
        private ICategoryResolverService _categoryResolverService { get; set; }

        public SharpTodoService(ITodoItemRepository todoItemRepository, ICategoryRepository categoryRepository, ICategoryResolverService categoryResolverService)
        {
            _todoItemRepository = todoItemRepository;
            _categoryRepository = categoryRepository;
            _categoryResolverService = categoryResolverService;
        }

        public TodoItem FindTodoItemById(int id)
        {
            return _todoItemRepository.FindById(id);
        }

        public IQueryable<TodoItem> FindAllTodoItems()
        {
            return _todoItemRepository.FindAll()
                .OrderBy(it => it.DueDate).ThenBy(it => it.Priority);
        }

        public IQueryable<TodoItem> FindTodoItemsByPriorityOrDate(int maxPriority, DateTime maxDate)
        {
            return _todoItemRepository.FindAll().Where(it => it.Priority <= maxPriority || it.DueDate <= maxDate)
                .OrderBy(it => it.DueDate).ThenBy(it => it.Priority);
        }

        public IQueryable<TodoItem> FindHotTodoItems()
        {
            return FindTodoItemsByPriorityOrDate(PriorotyMaxHot, DateTime.Now.AddDays(HotDays));
        }

        public void UpdateTodoItem(TodoItem item)
        {
            _todoItemRepository.SetModified(item);
            _todoItemRepository.SaveChanges();
        }

        public void AddTodoItem(string richDescription)
        {
            var item = new TodoItem(richDescription, _categoryResolverService);
            _todoItemRepository.Add(item);
            _todoItemRepository.SaveChanges();
        }

        public void AddTodoItem(TodoItem item)
        {
            _todoItemRepository.Add(item);
            _todoItemRepository.SaveChanges();
        }

        public void RemoveTodoItem(TodoItem item)
        {
            _todoItemRepository.Remove(item);
            _todoItemRepository.SaveChanges();
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
                _todoItemRepository.Dispose();
            }
        }

        ~SharpTodoService()
        {
            Dispose(false);
        }
    }
}
