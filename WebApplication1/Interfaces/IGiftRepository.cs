using ChineseAuctionProject.DTOs;

namespace ChineseAuctionProject.Interfaces
{
    public interface IGiftRepository
    {
        Task<IEnumerable<GiftDTOs.GiftReadDTO>> GetAllGiftsAsync();
        Task<GiftDTOs.GiftReadDTO?> GetGiftByIdAsync(int id);//יחזיר נל אם לא קיים
        Task<IEnumerable<GiftDTOs.GiftReadDTO>> GetGiftsByCategoryAsync(int categoryId);
        Task<GiftDTOs.GiftReadDTO> CreateGiftAsync(GiftDTOs.GiftCreateDTO createDto);
        Task<GiftDTOs.GiftReadDTO?> UpdateGiftAsync(int id, GiftDTOs.GiftUpdateDTO updateDto);
        Task<bool> DeleteGiftAsync(int id);
    }
}
