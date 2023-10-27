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
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository repository;
        private readonly IOrderDetailRepository detailRepository;

        public OrderService(IOrderRepository repository, IOrderDetailRepository detailRepository)
        {
            this.repository = repository;
            this.detailRepository = detailRepository;
        }

        public IEnumerable<Order> GetAllOrder()
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

        //public Task<Order> GetOrderByCustomerId(Guid id)
        //{
        //    try
        //    {

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        public async Task<bool> Payment(Guid cusId, List<OrderDetail> list)
        {
            try
            {
                Order order = new Order();
                order.CustomerId = cusId;
                order.TotalPrice = 0;
                order.OrderDate = DateTime.Now;
                order.RequiredDate = DateTime.Now;
                order.ShippedDate = DateTime.Now;
                order.IsDelete = false;
                var addOrders = await repository.Add(order);
                var odersDB = repository.GetAll(x => x.IsDelete == false).LastOrDefault();
                var addOrderDetail = false;

                if(addOrders)
                {
                    foreach (var orderDetail in list)
                    {
                        orderDetail.OrderId = odersDB.OrderId;
                        orderDetail.IsDelete = false;
                        addOrderDetail = await detailRepository.Add(orderDetail);
                    }
                }
                return addOrderDetail;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
