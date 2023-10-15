using ShopDB.Repositories.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDB.Service.Interface
{
    public interface IProductService
    {
        IEnumerable<Product> GetAllProduct();
        Task<Product> GetProductById(Guid id);
        Task<bool> AddProduct(Product product);
        Task<bool> UpdateProduct(Guid id, Product product);
        Task<bool> DeleteProduct(Guid id);
    }
}
