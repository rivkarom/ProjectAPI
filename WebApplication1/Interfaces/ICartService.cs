using static ChineseAuctionProject.DTOs.CartDTOs;

namespace ChineseAuctionProject.Interfaces
{
    public interface ICartService
    {
        Task<IEnumerable<CartReadDTO>> GetCartByUserIdAsync(string userId);
        Task<CartReadDTO?> GetCartItemAsync(int id);
        Task<CartReadDTO> AddToCartAsync(CartCreateDTO createDto);
        Task<CartReadDTO?> UpdateCartItemAsync(int id, CartUpdateDTO updateDto);
        Task<bool> RemoveFromCartAsync(int id);
        Task<bool> ConfirmCartAsync(string userId);
    }
}