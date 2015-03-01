using MB.SharpTodo.Core.Domain;
using MB.SharpTodo.Core.Domain.Interfaces;
using NUnit.Framework;
using Ninject;
using System.Linq;

namespace MB.SharpTodo.Core.Test.DataAccess
{
    [TestFixture]
    public class CategoryRepositoryTest
    {
        private IKernel _kernel;
        private ICategoryRepository _categoryRepository;

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
            _categoryRepository = _kernel.Get<ICategoryRepository>();
        }

        [TearDown]
        public void TearDown()
        {
            if (_categoryRepository != null)
                _categoryRepository.Dispose();
        }

        [Test]
        public void CreateCategory()
        {
            var c = new Category() {Description = "test category"};
            _categoryRepository.Add(c);
            _categoryRepository.SaveChanges();
            Assert.AreNotEqual(0, c.Id);
            Assert.AreEqual("test category", c.Description);
        }

        [Test]
        public void DeleteCategory()
        {
            var c = new Category() { Description = "test category" };

            var count = _categoryRepository.FindAll().Count();

            _categoryRepository.Add(c);
            _categoryRepository.SaveChanges();

            var count2 = _categoryRepository.FindAll().Count();

            Assert.AreEqual(count+1, count2);

            _categoryRepository.Remove(c);
            _categoryRepository.SaveChanges();

            var count3 = _categoryRepository.FindAll().Count();

            Assert.AreEqual(count, count3);
        }

        [Test]
        public void FindAll()
        {
            _categoryRepository.FindAll();
        }
    }
}
