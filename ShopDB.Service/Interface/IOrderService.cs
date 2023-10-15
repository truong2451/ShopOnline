using ShopDB.Repositories.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDB.Service.Interface
{
    public interface IOrderService
    {
        IEnumerable<Order> GetAllOrder();
        Task<Order> GetOrderById(Guid id);
        Task<bool> AddOrder(Order order);
        Task<bool> UpdateOrder(Guid id, Order order);
        Task<bool> DeleteOrder(Guid id);
    }
}
