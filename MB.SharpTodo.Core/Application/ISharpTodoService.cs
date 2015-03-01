using System;
using System.Linq;
using MB.SharpTodo.Core.Domain;

namespace MB.SharpTodo.Core.Application
{
    public interface ISharpTodoService : IDisposable
    {
        TodoItem FindTodoItemById(int id);
        IQueryable<TodoItem> FindAllTodoItems();
        IQueryable<TodoItem> FindTodoItemsByPriorityOrDate(int maxPriority, DateTime maxDate);
        IQueryable<TodoItem> FindHotTodoItems();

        void AddTodoItem(string richDescription);
        void AddTodoItem(TodoItem item);
        void UpdateTodoItem(TodoItem item);
        void RemoveTodoItem(TodoItem item);
    }
}