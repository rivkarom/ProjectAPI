using ChineseAuctionProject.DTOs;

namespace ChineseAuctionProject.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryResponsDto>> GetAllCategoryAsync();
        Task<CategoryResponsDto> GetCategoryByIdAsync(int id);
        Task<CategoryResponsDto> categoryCreateDTO(CategoryCreateDTO categoryCreateDTO);
        Task<CategoryResponsDto> categoryUpdateDTO(int id,CategoryUpdateDTO categoryUpdateDTO);
        
    }
}
