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
                CreatedAt = category.CreatedAt
            };
        }

        public async Task<CategoryResponsDto> categoryCreateDTO(CategoryCreateDTO categoryCreateDTO)
        {
            var category = new Category
            {
                Name = categoryCreateDTO.Name,
                Description = categoryCreateDTO.Description
            };

            var created = await _categoryRepository.CreateAsync(category);

            return new CategoryResponsDto
            {
                Id = created.Id,
                Name = created.Name,
                Description = created.Description,
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

            var updated = await _categoryRepository.UpdateAsync(category);

            return new CategoryResponsDto
            {
                Id = updated!.Id,
                Name = updated.Name,
                Description = updated.Description,
                CreatedAt = updated.CreatedAt
            };
        }
    }
}