using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MB.SharpTodo.Core.Domain.Interfaces;

namespace MB.SharpTodo.Core.Domain.Services
{
    public class CategoryResolverService : ICategoryResolverService
    {
        private ICategoryRepository _categoryRepository { get; set; }

        public CategoryResolverService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public Category FindOrCreateCategory(string description)
        {
            var result =
                _categoryRepository.FindAll()
                    .FirstOrDefault(it => it.Description.Trim().ToLower() == description.Trim().ToLower());

            return result ?? new Category {Description = description};
        }
    }
}
