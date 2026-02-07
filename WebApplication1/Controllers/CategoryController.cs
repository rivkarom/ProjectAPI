using ChineseAuctionProject.DTOs;
using ChineseAuctionProject.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChineseAuctionProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "manager")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryResponsDto>>> GetAll()
        {
            var categories = await _categoryService.GetAllCategoryAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryResponsDto>> GetById(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryResponsDto>> Create(CategoryCreateDTO createDto)
        {
            var category = await _categoryService.categoryCreateDTO(createDto);
            return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
        }

        [HttpPut("{id}")]//אולי צריך למחוק את זה כי זה מיותר
        public async Task<ActionResult<CategoryResponsDto>> Update(int id, CategoryUpdateDTO updateDto)
        {
            var category = await _categoryService.categoryUpdateDTO(id, updateDto);
            return Ok(category);
        }
    }
}
