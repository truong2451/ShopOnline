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
    public class ProductService : IProductService
    {
        private readonly IProductRepository repository;

        public ProductService(IProductRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<Product> GetAllProduct()
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

        public Task<Product> GetProductById(Guid id)
        {
            try
            {
                return repository.Get(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> AddProduct(Product product)
        {
            try
            {
                product.IsDelete = false;
                return await repository.Add(product);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteProduct(Guid id)
        {
            try
            {
                var item = await repository.Get(id);
                if (item != null)
                {
                    item.IsDelete = true;
                    return await repository.Update(item.ProductId, item);
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateProduct(Guid id, Product product)
        {
            try
            {
                var item = await repository.Get(id);
                if (item != null)
                {
                    item.CategoryId = product.CategoryId;
                    item.ProductName = product.ProductName;
                    item.Price = product.Price;
                    item.UnitInStock = product.UnitInStock;
                    item.Image = product.Image;
                    return await repository.Update(item.ProductId, item);
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
