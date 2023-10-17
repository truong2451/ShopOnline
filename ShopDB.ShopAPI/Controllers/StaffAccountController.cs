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
    public class StaffAccountController : ControllerBase
    {
        private readonly IStaffAccountService staffAccountService;
        private readonly IMapper mapper;

        public StaffAccountController(IStaffAccountService staffAccountService, IMapper mapper)
        {
            this.staffAccountService = staffAccountService;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddStaffAccount(StaffAccountVM model)
        {
            try
            {
                var role = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
                if(role == CommonValues.ADMIN)
                {
                    var account = mapper.Map<StaffAccount>(model);
                    var check = await staffAccountService.AddStaffAccount(account);
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
            catch(Exception ex)
            {
                return StatusCode(500, new
                {
                    Status = "Error",
                    Message = ex.Message,
                });
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStaffAccount(Guid id, StaffAccountUpdateVM model)
        {
            try
            {
                var role = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
                if (role == CommonValues.ADMIN)
                {
                    var accountDB = await staffAccountService.GetStaffAccountById(id); 
                    if(accountDB != null)
                    {
                        var account = mapper.Map<StaffAccount>(model);
                        var check = await staffAccountService.UpdateProfileStaff(accountDB.StaffId, account);
                        return check ? StatusCode(200, new
                        {
                            Message = "Update Success"
                        }) : StatusCode(500, new
                        {
                            Message = "Update Fail"
                        });
                    }
                    else
                    {
                        return StatusCode(404, new
                        {
                            Message = "Not Found Account"
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
                    Message = ex.Message,
                });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteStaffAccount(Guid id)
        {
            try
            {
                var role = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
                if (role == CommonValues.ADMIN)
                {
                    var accountDB = await staffAccountService.GetStaffAccountById(id);
                    if (accountDB != null)
                    {
                        var check = await staffAccountService.DeleteStaffAccount(id);
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
                            Message = "Not Found Account"
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
                    Message = ex.Message,
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(string oldPassword, string newPassword)
        {
            try
            {
                var role = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
                var accountStaffId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                if(role == CommonValues.STAFF)
                {
                    var accountDB = await staffAccountService.GetStaffAccountById(Guid.Parse(accountStaffId));
                    if(accountDB != null)
                    {
                        var check = await staffAccountService.ChangePasswordStaff(Guid.Parse(accountStaffId), oldPassword, newPassword);
                        return check ? StatusCode(200, new
                        {
                            Message = "Change Password Success"
                        }) : StatusCode(500, new
                        {
                            Message = "Change Password Fail"
                        });
                    }
                    else
                    {
                        return StatusCode(404, new
                        {
                            Message = "Not Found Account"
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
            catch(Exception ex)
            {
                return StatusCode(500, new
                {
                    Status = "Error",
                    Message = ex.Message
                });
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProfile(StaffAccountUpdateVM model)
        {
            try
            {
                var role = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
                var accountStaffId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                if (role == CommonValues.STAFF)
                {
                    var accountDB = await staffAccountService.GetStaffAccountById(Guid.Parse(accountStaffId));
                    if (accountDB != null)
                    {
                        var account = mapper.Map<StaffAccount>(model);
                        var check = await staffAccountService.UpdateProfileStaff(Guid.Parse(accountStaffId), account);
                        return check ? StatusCode(200, new
                        {
                            Message = "Update Success"
                        }) : StatusCode(500, new
                        {
                            Message = "Update Fail"
                        });
                    }
                    else
                    {
                        return StatusCode(404, new
                        {
                            Message = "Not Found Account"
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
                    Message = ex.Message,
                });
            }
        }

        public async Task<IActionResult> Login(string username, string password)
        {
            try
            {
                var checkAccount = staffAccountService.CheckLogin(username);
                if (checkAccount != null)
                {
                    if(checkAccount.Role == 0 || checkAccount.Role == 1)
                    {
                        if (checkAccount.Password == password)
                        {
                            return StatusCode(200, new
                            {
                                Message = "Login Success"
                            });
                        }
                        else
                        {
                            return StatusCode(400, new
                            {
                                Message = "Incorrect Password"
                            });
                        }
                    }
                    else
                    {
                        return StatusCode(400, new
                        {
                            Message = "You do not have permission"
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
                    Message = ex.Message,
                });
            }
        }
    }
}
