using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopDB.Repositories.EntityModel;
using ShopDB.Service.Interface;
using System.Security.Claims;

namespace ShopDB.ShopAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;
        private readonly IMapper mapper;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            this.orderService = orderService;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return StatusCode(200, new
                {
                    Message = "Success",
                    Data = orderService.GetAllOrder()
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Status = "Error",
                    Message = ex.Message,
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatePayment(List<OrderDetail> list)
        {
            try
            {
                var role = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
                var cusId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                if (role == null)
                {
                    return StatusCode(401, new
                    {
                        Status = "Error",
                        Message = "You Are Not Login"
                    });
                }
                else
                {
                    if (role == CommonValues.CUSTOMER)
                    {
                        var listOrderDetail = new List<OrderDetail>();
                        if(list != null)
                        {
                            foreach (var item in list)
                            {
                                listOrderDetail.Add(new OrderDetail
                                {
                                    ProductId = item.ProductId,
                                    Price = item.Price,
                                    Discount = item.Discount,
                                    Quantity = item.Quantity,
                                });
                            }
                        }

                        var check = await orderService.Payment(Guid.Parse(cusId), listOrderDetail);
                        return check ? StatusCode(200, new
                        {
                            Message = "Add Success"
                        }) : StatusCode(500, new
                        {
                            Message = "Add Fail"
                        });
                    }
                    else
                    {
                        return StatusCode(403, new
                        {
                            Status = "Error",
                            Message = "Role Denied"
                        });
                    }
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Status = "Error",
                    Message = ex.Message,
                });
            }
        }
    }
}
