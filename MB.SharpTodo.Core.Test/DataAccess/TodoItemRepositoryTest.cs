using MB.SharpTodo.Core.Domain;
using MB.SharpTodo.Core.Domain.Interfaces;
using NUnit.Framework;
using System;
using System.Linq;
using Ninject;

namespace MB.SharpTodo.Core.Test.DataAccess
{
    [TestFixture]
    public class TodoItemRepositoryTest
    {
        private IKernel _kernel;
        private ITodoItemRepository _todoItemRepository;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _kernel = new StandardKernel(new SharpTodoNinjectModule());
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            if (_kernel!=null) 
                _kernel.Dispose();
        }

        [SetUp]
        public void SetUp()
        {
            _todoItemRepository = _kernel.Get<ITodoItemRepository>();
        }

        [TearDown]
        public void TearDown()
        {
            if (_todoItemRepository != null) 
                _todoItemRepository.Dispose();
        }

        [Test]
        public void CreateTodoItem()
        {
            var t = new TodoItem { Priority = 4, Description = "test", DueDate = new DateTime(1999, 9, 13) };
            _todoItemRepository.Add(t);
            _todoItemRepository.SaveChanges();
            Assert.AreNotEqual(0, t.Id);
            Assert.AreEqual("test", t.Description);
            Assert.AreEqual(new DateTime(1999, 9, 13), t.DueDate);
            Assert.AreEqual(4, t.Priority);
        }

        [Test]
        public void LoadSingleItemProprieties()
        {
            var t = new TodoItem
            {
                Priority = 4, 
                Description = "test", 
                DueDate = new DateTime(1999, 9, 13),
                Category = new Category { Description = "new category"}
            };

            t.AddComment(new TodoComment { Text = "comment 1" });
            t.AddComment(new TodoComment { Text = "comment 2" });

            _todoItemRepository.Add(t);
            _todoItemRepository.SaveChanges();
            Assert.AreNotEqual(0, t.Id);

            var newRepo = _kernel.Get<ITodoItemRepository>();

            var t2 = newRepo.FindById(t.Id);

            Assert.AreEqual(t.Category.Id, t2.Category.Id);
            Assert.AreEqual(2, t2.Comments.Count);
            Assert.AreEqual(t.Comments.ToArray()[0].Id, t2.Comments.ToArray()[0].Id);
            Assert.AreEqual(t.Comments.ToArray()[1].Id, t2.Comments.ToArray()[1].Id);

            Assert.AreSame(t2, t2.Comments.ToArray()[0].TodoItem);
        }

        [Test]
        public void LoadInListItemProprieties()
        {
            var t = new TodoItem
            {
                Priority = 4,
                Description = "test",
                DueDate = new DateTime(1999, 9, 13),
                Category = new Category { Description = "new category" }
            };

            t.AddComment(new TodoComment { Text = "comment 1" });
            t.AddComment(new TodoComment { Text = "comment 2" });

            _todoItemRepository.Add(t);
            _todoItemRepository.SaveChanges();
            Assert.AreNotEqual(0, t.Id);

            var newRepo = _kernel.Get<ITodoItemRepository>();

            var t2 = newRepo.FindAll().First(it => it.Id == t.Id);

            Assert.AreEqual(t.Category.Id, t2.Category.Id);
            Assert.AreEqual(2, t2.Comments.Count);
            Assert.AreEqual(t.Comments.ToArray()[0].Id, t2.Comments.ToArray()[0].Id);
            Assert.AreEqual(t.Comments.ToArray()[1].Id, t2.Comments.ToArray()[1].Id);

            Assert.AreSame(t2, t2.Comments.ToArray()[0].TodoItem);
        }

        [Test]
        public void CascadeDelete()
        {
            var t = new TodoItem
            {
                Priority = 4,
                Description = "test",
                DueDate = new DateTime(1999, 9, 13),
                Category = new Category { Description = "new category" }
            };

            t.AddComment(new TodoComment { Text = "comment 1" });
            t.AddComment(new TodoComment { Text = "comment 2" });

            _todoItemRepository.Add(t);
            _todoItemRepository.SaveChanges();
            
            _todoItemRepository.Remove(t);
            _todoItemRepository.SaveChanges();

            var newRepo = _kernel.Get<ITodoItemRepository>();

            Assert.IsNull(newRepo.FindById(t.Id));
        }

        [Test]
        public void CreateTodoItemWithCategory()
        {
            var t = new TodoItem { Priority = 4, Description = "test", DueDate = new DateTime(1999, 9, 13) };
            t.Category = new Category { Description = "a test category" };
            _todoItemRepository.Add(t);
            _todoItemRepository.SaveChanges();
            Assert.AreNotEqual(0, t.Id);
            Assert.AreNotEqual(0, t.Category.Id);
        }

        [Test]
        public void CreateTodoItemWithComments()
        {
            var t = new TodoItem { Priority = 4, Description = "test", DueDate = new DateTime(1999, 9, 13) };
            t.AddComment(new TodoComment() { Text = "test comment 1" });
            t.AddComment(new TodoComment() { Text = "test comment 2" });
            _todoItemRepository.Add(t);
            _todoItemRepository.SaveChanges();
            Assert.AreNotEqual(0, t.Id);

            foreach (var comment in t.Comments)
            {
                Assert.AreNotEqual(0, comment.Id);
            }
        }

        [Test]
        public void DeleteTodoItem()
        {
            var t = new TodoItem {Description = "test"};

            var count = _todoItemRepository.FindAll().Count();

            _todoItemRepository.Add(t);
            _todoItemRepository.SaveChanges();

            var count2 = _todoItemRepository.FindAll().Count();

            Assert.AreEqual(count+1, count2);

            _todoItemRepository.Remove(t);
            _todoItemRepository.SaveChanges();

            var count3 = _todoItemRepository.FindAll().Count();

            Assert.AreEqual(count, count3);
        }
    }
}
