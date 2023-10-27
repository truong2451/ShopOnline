using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopDB.Repositories.EntityModel;
using ShopDB.Service.Interface;
using ShopDB.ShopAPI.ModelView;
using System.Security.Claims;

namespace ShopDB.ShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService customerService;
        private readonly IMapper mapper;

        public CustomerController(ICustomerService customerService, IMapper mapper)
        {
            this.customerService = customerService;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(CustomerVM model)
        {
            try
            {
                var role = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
                if (role == CommonValues.CUSTOMER)
                {
                    var customer = mapper.Map<Customer>(model);
                    var check = await customerService.SignUp(customer);
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
                    return StatusCode(400, new
                    {
                        Status = "Error",
                        Message = "Role Denied"
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Status = "Error",
                    Message = ex.Message
                });
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProfileCustomer(CustomerUpdateVM model)
        {
            try
            {
                var role = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
                var customerId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                if (role == CommonValues.CUSTOMER)
                {
                    var customerDB = customerService.GetCustomerById(Guid.Parse(customerId));
                    if (customerDB != null)
                    {
                        var customer = mapper.Map<Customer>(model);
                        var check = await customerService.UpdateCustomer(Guid.Parse(customerId), customer);
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
                        return StatusCode(404, new
                        {
                            Message = "Not Found Customer"
                        });
                    }
                }
                else
                {
                    return StatusCode(400, new
                    {
                        Status = "Error",
                        Message = "Role Denied"
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Status = "Error",
                    Message = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> LoginCustomer(string username, string password)
        {
            try
            {
                var check = customerService.CheckLogin(username);
                if (check != null)
                {
                    if (check.Password == password)
                    {
                        return StatusCode(200, new
                        {
                            Message = "Login Customer Success",
                            Role = "Customer",
                            Data = new {},
                            Token = JWTMange.GetToken(check.CustomerId.ToString(), "Customer")
                        });
                    }
                    else
                    {
                        return StatusCode(400, new
                        {
                            Message = "Password Invalid"
                        });
                    }
                }
                else
                {
                    return StatusCode(404, new
                    {
                        Message = "Not Found Account"
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Status = "Error",
                    Message = ex.Message
                });
            }
        }

        [HttpPut]
        public async Task<IActionResult> ChangePwd(string oldPwd, string newPwd)
        {
            try
            {
                var role = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
                if (role == CommonValues.CUSTOMER)
                {
                    var cusId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                    var check =await customerService.ChangePassword(Guid.Parse(cusId), oldPwd, newPwd);
                    return check ? StatusCode(200, new
                    {
                        Message = "Change Password Success"
                    }) : StatusCode(200, new
                    {
                        Message = "Change Password Fail"
                    });
                }
                else
                {
                    return StatusCode(500, new
                    {
                        Status = "Error",
                        Message = "Role Denied"
                    });
                }    
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Status = "Error",
                    Message = ex.Message
                });
            }
        }

        //---------------------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------------------
        [HttpGet]
        public async Task<IActionResult> GetAllCustomer()
        {
            try
            {
                return StatusCode(200, new
                {
                    Status = "Success",
                    Data = customerService.GetAllCustomer()
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Status = "Error",
                    Message = ex.Message
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomerById(Guid id)
        {
            try
            {
                return StatusCode(200, new
                {
                    Status = "Success",
                    Data = await customerService.GetCustomerById(id)
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Status = "Error",
                    Message = ex.Message
                });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            try
            {
                var role = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
                if (role == CommonValues.ADMIN)
                {
                    var customer = await customerService.GetCustomerById(id);
                    if (customer != null)
                    {
                        var check = await customerService.DeleteCustomer(id);
                        return check ? StatusCode(200, new
                        {
                            Message = "Delete Success"
                        }) : StatusCode(500, new
                        {
                            Message = "Delete Fail"
                        });
                    }
                    else
                    {
                        return StatusCode(404, new
                        {
                            Message = "Not Found Customer"
                        });
                    }
                }
                else
                {
                    return StatusCode(400, new
                    {
                        Status = "Error",
                        Message = "Role Denied"
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Status = "Error",
                    Message = ex.Message
                });
            }
        }


    }
}
