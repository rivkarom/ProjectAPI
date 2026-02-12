using ChineseAuctionProject.Interfaces;
using ChineseAuctionProject.Models;
using StoreApi.Data;
using Microsoft.EntityFrameworkCore;
using static ChineseAuctionProject.DTOs.CartDTOs;

namespace ChineseAuctionProject.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;

        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CartReadDTO>> GetCartByUserIdAsync(string userId)
        {
            return await _context.Carts
                .Where(c => c.UserId == userId && !c.IsConfirmed)
                .Include(c => c.Gift)
                .Select(c => new CartReadDTO
                {
                    Id = c.Id,
                    GiftId = c.GiftId,
                    GiftName = c.Gift != null ? c.Gift.Name : string.Empty,
                    TicketsCount = c.TicketsCount,
                    IsConfirmed = c.IsConfirmed
                })
                .ToListAsync();
        }

        public async Task<CartReadDTO?> GetCartItemAsync(int id)
        {
            var cart = await _context.Carts
                .Include(c => c.Gift)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cart == null) return null;

            return new CartReadDTO
            {
                Id = cart.Id,
                GiftId = cart.GiftId,
                GiftName = cart.Gift?.Name ?? string.Empty,
                TicketsCount = cart.TicketsCount,
                IsConfirmed = cart.IsConfirmed
            };
        }

        public async Task<CartReadDTO> AddToCartAsync(CartCreateDTO createDto)
        {
            var gift = await _context.Gifts.FindAsync(createDto.GiftId);
            if (gift == null || gift.IsRaffled)
            {
                throw new InvalidOperationException("Cannot add raffled gift to cart");
            }

            var existing = await _context.Carts.FirstOrDefaultAsync(c => c.UserId == createDto.UserId && c.GiftId == createDto.GiftId && !c.IsConfirmed);
            if (existing != null)
            {
                existing.TicketsCount += createDto.TicketsCount;
                await _context.SaveChangesAsync();
                await _context.Entry(existing).Reference(c => c.Gift).LoadAsync();
                return new CartReadDTO
                {
                    Id = existing.Id,
                    GiftId = existing.GiftId,
                    GiftName = existing.Gift?.Name ?? string.Empty,
                    TicketsCount = existing.TicketsCount,
                    IsConfirmed = existing.IsConfirmed
                };
            }
            else
            {
                var cart = new Cart
                {
                    UserId = createDto.UserId,
                    GiftId = createDto.GiftId,
                    TicketsCount = createDto.TicketsCount,
                    IsConfirmed = false
                };
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();

                await _context.Entry(cart).Reference(c => c.Gift).LoadAsync();

                return new CartReadDTO
                {
                    Id = cart.Id,
                    GiftId = cart.GiftId,
                    GiftName = cart.Gift?.Name ?? string.Empty,
                    TicketsCount = cart.TicketsCount,
                    IsConfirmed = cart.IsConfirmed
                };
            }
        }

        public async Task<CartReadDTO> AddToCartAsync(string userId, int giftId, int quantity)
        {
            var gift = await _context.Gifts.FindAsync(giftId);
            if (gift == null || gift.IsRaffled)
            {
                throw new InvalidOperationException("Cannot add raffled gift to cart");
            }

            var existing = await _context.Carts.FirstOrDefaultAsync(c => c.UserId == userId && c.GiftId == giftId && !c.IsConfirmed);
            if (existing != null)
            {
                existing.TicketsCount += quantity;
                await _context.SaveChangesAsync();
                await _context.Entry(existing).Reference(c => c.Gift).LoadAsync();
                return new CartReadDTO
                {
                    Id = existing.Id,
                    GiftId = existing.GiftId,
                    GiftName = existing.Gift?.Name ?? string.Empty,
                    TicketsCount = existing.TicketsCount
                };
            }
            else
            {
                var cartItem = new Cart
                {
                    UserId = userId,
                    GiftId = giftId,
                    TicketsCount = quantity
                };
                _context.Carts.Add(cartItem);
                await _context.SaveChangesAsync();

                await _context.Entry(cartItem).Reference(c => c.Gift).LoadAsync();

                return new CartReadDTO
                {
                    Id = cartItem.Id,
                    GiftId = cartItem.GiftId,
                    GiftName = cartItem.Gift?.Name ?? string.Empty,
                    TicketsCount = cartItem.TicketsCount
                };
            }
        }

        public async Task<CartReadDTO?> UpdateCartItemAsync(int id, CartUpdateDTO updateDto)
        {
            var cart = await _context.Carts.FindAsync(id);
            if (cart == null || cart.IsConfirmed) return null;

            if (updateDto.TicketsCount.HasValue)
            {
                cart.TicketsCount = updateDto.TicketsCount.Value;
            }

            await _context.SaveChangesAsync();

            await _context.Entry(cart).Reference(c => c.Gift).LoadAsync();

            return new CartReadDTO
            {
                Id = cart.Id,
                GiftId = cart.GiftId,
                GiftName = cart.Gift?.Name ?? string.Empty,
                TicketsCount = cart.TicketsCount,
                IsConfirmed = cart.IsConfirmed
            };
        }

        public async Task<bool> RemoveFromCartAsync(int id)
        {
            var cart = await _context.Carts.FindAsync(id);
            if (cart == null || cart.IsConfirmed) return false;

            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ConfirmCartAsync(string userId)
        {
            var cartItems = await _context.Carts
                .Where(c => c.UserId == userId && !c.IsConfirmed)
                .ToListAsync();

            if (!cartItems.Any()) return false;

            foreach (var item in cartItems)
            {
                var order = new OrderManagement
                {
                    UserId = item.UserId,
                    GiftId = item.GiftId,
                    TicketsCount = item.TicketsCount,
                    IsPaid = true
                };
                _context.OrderManagements.Add(order);
                item.IsConfirmed = true;
            }

            await _context.SaveChangesAsync();
            return true;
        }
    }
}