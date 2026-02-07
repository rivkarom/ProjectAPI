using ChineseAuctionProject.Interfaces;
using static ChineseAuctionProject.DTOs.CartDTOs;

namespace ChineseAuctionProject.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly ILogger<CartService> _logger;

        public CartService(ICartRepository cartRepository, ILogger<CartService> logger)
        {
            _cartRepository = cartRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<CartReadDTO>> GetCartByUserIdAsync(string userId)
        {
            return await _cartRepository.GetCartByUserIdAsync(userId);
        }

        public async Task<CartReadDTO?> GetCartItemAsync(int id)
        {
            return await _cartRepository.GetCartItemAsync(id);
        }

        public async Task<CartReadDTO> AddToCartAsync(CartCreateDTO createDto)
        {
            return await _cartRepository.AddToCartAsync(createDto);
        }

        public async Task<CartReadDTO?> UpdateCartItemAsync(int id, CartUpdateDTO updateDto)
        {
            return await _cartRepository.UpdateCartItemAsync(id, updateDto);
        }

        public async Task<bool> RemoveFromCartAsync(int id)
        {
            return await _cartRepository.RemoveFromCartAsync(id);
        }

        public async Task<bool> ConfirmCartAsync(string userId)
        {
            return await _cartRepository.ConfirmCartAsync(userId);
        }
    }
}