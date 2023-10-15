using ShopDB.Repositories.EntityModel;
using ShopDB.Repositories.Repository.Interface;
using ShopDB.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDB.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository repository;

        public CategoryService(ICategoryRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<Category> GetAllCategory()
        {
            try
            {
                return repository.GetAll(x => x.IsDelete == false);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Category> GetCategoryById(Guid id)
        {
            try
            {
                return await repository.Get(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<bool> AddCategory(Category category)
        {
            try
            {
                category.IsDelete = false;
                return repository.Add(category);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteCategory(Guid id)
        {
            try
            {
                var item = await repository.Get(id);
                if (item != null)
                {
                    item.IsDelete = true;
                    return await repository.Update(item.CategoryId, item);
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateCategory(Guid id, Category category)
        {
            try
            {
                var item = await repository.Get(id);
                if (item != null)
                {
                    item.CategoryName = category.CategoryName;
                    return await repository.Update(id, item);
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
