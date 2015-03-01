using MB.SharpTodo.Core.Domain;
using MB.SharpTodo.Core.Domain.Services;
using NUnit.Framework;

namespace MB.SharpTodo.Core.Test.Domain
{
    [TestFixture]
    class TodoItemTest
    {
        [Test]
        [ExpectedException("MB.SharpTodo.Core.SharpTodoException")]
        public void PriorityZero()
        {
            var t = new TodoItem();
            t.Priority = 0;
        }

        [Test]
        public void PriorityNullOrGreaterThanZero()
        {
            var t = new TodoItem();
            t.Priority = null;
            t.Priority = 1;
        }

        [Test]
        public void Parse1()
        {
            var categoryResolverService = new Moq.Mock<ICategoryResolverService>();
           
            var description = "!not ! 4!6 !45t 5! do#ing something";
            var t = new TodoItem(description, categoryResolverService.Object);

            Assert.IsNull(t.Priority);
            Assert.AreEqual(description, t.Description);
        }

        [Test]
        public void Parse2()
        {
            var categoryResolverService = new Moq.Mock<ICategoryResolverService>();
            categoryResolverService.Setup(service => service.FindOrCreateCategory("category1")).Returns(new Category { Description = "category1" });

            var t = new TodoItem("!4 doing something #category1", categoryResolverService.Object);

            Assert.AreEqual(4, t.Priority);
            Assert.AreEqual("doing something", t.Description);
            Assert.IsNotNull(t.Category);
            Assert.AreEqual("category1", t.Category.Description);
        }
    }
}
