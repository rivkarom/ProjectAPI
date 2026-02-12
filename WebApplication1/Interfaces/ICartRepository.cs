using static ChineseAuctionProject.DTOs.CartDTOs;

namespace ChineseAuctionProject.Interfaces
{
    public interface ICartRepository
    {
        Task<IEnumerable<CartReadDTO>> GetCartByUserIdAsync(string userId);
        Task<CartReadDTO?> GetCartItemAsync(int id);
        Task<CartReadDTO> AddToCartAsync(CartCreateDTO createDto);
        Task<CartReadDTO> AddToCartAsync(string userId, int giftId, int quantity);
        Task<CartReadDTO?> UpdateCartItemAsync(int id, CartUpdateDTO updateDto);
        Task<bool> RemoveFromCartAsync(int id);
        Task<bool> ConfirmCartAsync(string userId);
    }
}