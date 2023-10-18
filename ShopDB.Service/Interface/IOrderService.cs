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
        //Task<Order> GetOrderByCustomerId(Guid id);
        Task<bool> Payment(List<OrderDetail> list);
    }
}
