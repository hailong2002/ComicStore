using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }
        public List<Category> GetAllCategories()
        {
            return _repository.GetAllCategories();
        }

        public Category GetCategory(int id)
        {
            return _repository.GetCategory(id);
        }

        public void AddCategory(Category category)
        {
            _repository.AddCategory(category);
        }

        public void EditCategory(Category category)
        {
            _repository.EditCategory(category);
        }

        public void DeleteCategory(int id)
        {
            _repository.DeleteCategory(id);
        }
        public bool CategoryExists(int id)
        {
            return _repository.CategoryExists(id);
        }
    }
}
