using System;
using System.Linq;
using MB.SharpTodo.Core.Application;
using MB.SharpTodo.Core.Domain;
using MB.SharpTodo.Core.Domain.Interfaces;
using Ninject;
using NUnit.Framework;

namespace MB.SharpTodo.Core.Test.Application
{
    [TestFixture]
    public class SharpTodoServiceTest
    {
        [Test]
        public void FindByPriorityAndDate()
        {
            var testData = new TodoItem[]
            {
                new TodoItem {Id = 10, Description = "test10", Priority = 2, DueDate = new DateTime(2030, 1, 1)}, // yes
                new TodoItem {Id = 30, Description = "test30", Priority = 8, DueDate = new DateTime(2020, 12, 10)}, // yes
                new TodoItem {Id = 40, Description = "test40", Priority = 8, DueDate = new DateTime(2050, 1, 1)}, // no
                new TodoItem {Id = 20, Description = "test20", Priority = 1, DueDate = new DateTime(2030, 1, 1)} // yes
            };

            var todoItemRepository = new Moq.Mock<ITodoItemRepository>();
            todoItemRepository.Setup(repo => repo.FindAll()).Returns(testData.AsQueryable());

            //todoItemRepository.Setup(repo => repo.Find(It.IsAny<Expression<Func<TodoItem, bool>>>())).Returns(testData);

            using (var kernel = new StandardKernel(new SharpTodoNinjectModule()))
            {
                kernel.Rebind<ITodoItemRepository>().ToMethod(context => todoItemRepository.Object);
                var service = kernel.Get<ISharpTodoService>();
                var todoItems = service.FindTodoItemsByPriorityOrDate(5, new DateTime(2020, 12, 12)).ToList();

                Assert.AreEqual(3, todoItems.Count);

                Assert.AreEqual(30, todoItems[0].Id);
                Assert.AreEqual(20, todoItems[1].Id);
                Assert.AreEqual(10, todoItems[2].Id);
            }
        }
    }
}
