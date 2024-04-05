using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public interface ICategoryRepository
    {
        List<Category> GetAllCategories();
        Category GetCategory(int id);
        void AddCategory(Category category);
        void EditCategory(Category category);
        void DeleteCategory(int id);

        bool CategoryExists(int id);
    }
}
