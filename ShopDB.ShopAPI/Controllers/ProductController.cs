using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopDB.Repositories.EntityModel;
using ShopDB.Service.Interface;
using ShopDB.ShopAPI.ModelView;

namespace ShopDB.ShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly IMapper mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            this.productService = productService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string search)
        {
            try
            {
                if(search != null)
                {
                    return StatusCode(200, new
                    {
                        Status = "Search Success",
                        Data = productService.Search(search)
                    });
                }
                else
                {
                    return StatusCode(200, new
                    {
                        Status = "Success",
                        Data = productService.GetAllProduct()
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

        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                return StatusCode(200, new
                {
                    Message = "Success",
                    Data = await productService.GetProductById(id)
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
        public async Task<IActionResult> AddProduct(ProductVM model)
        {
            try
            {
                var product = mapper.Map<Product>(model);
                var check = await productService.AddProduct(product);
                return check ? StatusCode(200, new
                {
                    Message = "Add Success"
                }) : StatusCode(500, new
                {
                    Message = "Add Fail"
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

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(Guid id, ProductVM model)
        {
            try
            {
                var productDB = await productService.GetProductById(id);
                if(productDB != null)
                {
                    var product = mapper.Map<Product>(model);
                    var check = await productService.UpdateProduct(id, product);
                    return check ? StatusCode(200, new
                    {
                        Message = "Udpate Success"
                    }) : StatusCode(500, new
                    {
                        Message = "Update Fail"
                    });
                }
                else
                {
                    return StatusCode(404, new
                    {
                        Message = "Not Found Product"
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
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            try
            {
                var product = await productService.GetProductById(id);
                if (product != null)
                {
                    var check = await productService.DeleteProduct(id);
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
                        Message = "Not Found Product"
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
