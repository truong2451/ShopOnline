﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopDB.Repositories.EntityModel;
using ShopDB.Service.Interface;
using ShopDB.ShopAPI.ModelView;

namespace ShopDB.ShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;
        private readonly IMapper mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            this.categoryService = categoryService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return StatusCode(200, new
                {
                    Message = "Success",
                    Data = categoryService.GetAllCategory()
                });
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

        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                return StatusCode(200, new
                {
                    Message = "Success",
                    Data = await categoryService.GetCategoryById(id)
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

        [HttpPost]
        public async Task<IActionResult> Add(CategoryVM model)
        {
            try
            {
                var category = mapper.Map<Category>(model);
                var check = await categoryService.AddCategory(category);
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
                    Message = ex.Message
                });
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(Guid id, CategoryVM model)
        {
            try
            {
                var category = await categoryService.GetCategoryById(id);
                if(category != null)
                {
                    var map = mapper.Map<Category>(model);
                    var check = await categoryService.UpdateCategory(id, map);
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
                        Message = "Not Found Category"
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

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var category = await categoryService.GetCategoryById(id);
                if( category != null )
                {
                    var check = await categoryService.DeleteCategory(id);
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
                        Message = "Not Found Category"
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
