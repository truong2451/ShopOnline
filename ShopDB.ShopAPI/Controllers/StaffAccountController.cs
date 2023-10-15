using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopDB.Service.Interface;

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
    }
}
