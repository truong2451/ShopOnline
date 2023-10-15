using ShopDB.Repositories.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDB.Service.Interface
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAllCategory();
        Task<Category> GetCategoryById(Guid id);
        Task<bool> AddCategory(Category category);
        Task<bool> UpdateCategory(Guid id, Category category);
        Task<bool> DeleteCategory(Guid id);
    }
}
