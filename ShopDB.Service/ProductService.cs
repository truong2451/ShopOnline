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
                CheckValidation(product);
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
                CheckValidation(product);
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

        public IEnumerable<Product>? Search(string search)
        {
            try
            {
                List<Product> list = new List<Product>();
                if (decimal.TryParse(search, out decimal price))
                {
                    var searchWithPrice = repository.GetAll(x => x.Price == price);
                    foreach (var item in searchWithPrice)
                    {
                        list.Add(item);
                    }
                }
                else
                {
                    var searchWithName = repository.GetAll(x => x.ProductName.Contains(search));
                    foreach (var item in searchWithName)
                    {
                        list.Add(item);
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Product CheckValidation(Product product)
        {
            try
            {
                if (string.IsNullOrEmpty(product.ProductName))
                {
                    throw new Exception("Product Name Invalid");
                }
                if (product.Price < 0)
                {
                    throw new Exception("Price Invalid");
                }
                if (product.UnitInStock == null || product.UnitInStock < 0)
                {
                    throw new Exception("UnitInStock Invalid");
                }

                return product;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
