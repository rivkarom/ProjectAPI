using ChineseAuctionProject.DTOs;
namespace ChineseAuctionProject.Interfaces
{
    public interface IGiftService
    {
        Task<IEnumerable<GiftDTOs.GiftReadDTO>> GetAllGiftsAsync();
        Task<GiftDTOs.GiftReadDTO?> GetGiftByIdAsync(int id);//יחזיר נל אם לא קיים
        Task<GiftDTOs.GiftReadDTO> CreateGiftAsync(DTOs.GiftDTOs.GiftCreateDTO createDto);
        Task<GiftDTOs.GiftReadDTO?> UpdateGiftAsync(int id, DTOs.GiftDTOs.GiftUpdateDTO updateDto);
        Task<bool> DeleteGiftAsync(int id);
    }
}
