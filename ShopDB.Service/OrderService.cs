using ShopDB.Repositories.EntityModel;
using ShopDB.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDB.Service
{
    public class OrderService : IOrderService
    {
        public Task<bool> AddOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteOrder(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetAllOrder()
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetOrderById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateOrder(Guid id, Order order)
        {
            throw new NotImplementedException();
        }
    }
}
