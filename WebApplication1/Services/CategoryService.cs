using ChineseAuctionProject.DTOs;
using ChineseAuctionProject.Interfaces;
using ChineseAuctionProject.Models;

namespace ChineseAuctionProject.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(ICategoryRepository categoryRepository, ILogger<CategoryService> logger)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<CategoryResponsDto>> GetAllCategoryAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return categories.Select(c => new CategoryResponsDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Icon = c.Icon,
                CreatedAt = c.CreatedAt
            });
        }

        public async Task<CategoryResponsDto> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                throw new KeyNotFoundException($"Category with id {id} not found");
            }

            return new CategoryResponsDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                Icon = category.Icon,
                CreatedAt = category.CreatedAt
            };
        }

        public async Task<CategoryResponsDto> categoryCreateDTO(CategoryCreateDTO categoryCreateDTO)
        {
            var category = new Category
            {
                Name = categoryCreateDTO.Name,
                Description = categoryCreateDTO.Description,
                Icon = categoryCreateDTO.Icon
            };

            var created = await _categoryRepository.CreateAsync(category);

            return new CategoryResponsDto
            {
                Id = created.Id,
                Name = created.Name,
                Description = created.Description,
                Icon = created.Icon,
                CreatedAt = created.CreatedAt
            };
        }

        public async Task<CategoryResponsDto> categoryUpdateDTO(int id, CategoryUpdateDTO categoryUpdateDTO)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                throw new KeyNotFoundException($"Category with id {id} not found");
            }

            if (!string.IsNullOrEmpty(categoryUpdateDTO.Name))
            {
                category.Name = categoryUpdateDTO.Name;
            }

            if (!string.IsNullOrEmpty(categoryUpdateDTO.Description))
            {
                category.Description = categoryUpdateDTO.Description;
            }

            if (!string.IsNullOrEmpty(categoryUpdateDTO.Icon))
            {
                category.Icon = categoryUpdateDTO.Icon;
            }

            var updated = await _categoryRepository.UpdateAsync(category);

            return new CategoryResponsDto
            {
                Id = updated!.Id,
                Name = updated.Name,
                Description = updated.Description,
                Icon = updated.Icon,
                CreatedAt = updated.CreatedAt
            };
        }
    }
}