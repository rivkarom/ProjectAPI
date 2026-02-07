using ChineseAuctionProject.Interfaces;
using ChineseAuctionProject.Models;
using StoreApi.Data;
using Microsoft.EntityFrameworkCore;
using static ChineseAuctionProject.DTOs.WinnerDTOs;

namespace ChineseAuctionProject.Repositories
{
    public class WinnerRepository : IWinnerRepository
    {
        private readonly ApplicationDbContext _context;

        public WinnerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WinnerReadDTO>> GetWinnersByGiftIdAsync(int giftId)
        {
            return await _context.Winners
                .Where(w => w.GiftId == giftId)
                .Include(w => w.Gift)
                .Include(w => w.User)
                .Select(w => new WinnerReadDTO
                {
                    Id = w.Id,
                    GiftId = w.GiftId,
                    GiftName = w.Gift != null ? w.Gift.Name : string.Empty,
                    UserId = w.UserId,
                    UserName = w.User != null ? w.User.UserName : string.Empty,
                    UserEmail = w.User != null ? w.User.Email : string.Empty,
                    WonAt = w.WonAt
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<WinnerReadDTO>> GetAllWinnersAsync()
        {
            return await _context.Winners
                .Include(w => w.Gift)
                .Include(w => w.User)
                .Select(w => new WinnerReadDTO
                {
                    Id = w.Id,
                    GiftId = w.GiftId,
                    GiftName = w.Gift != null ? w.Gift.Name : string.Empty,
                    UserId = w.UserId,
                    UserName = w.User != null ? w.User.UserName : string.Empty,
                    UserEmail = w.User != null ? w.User.Email : string.Empty,
                    WonAt = w.WonAt
                })
                .ToListAsync();
        }

        public async Task<WinnerReadDTO> CreateWinnerAsync(int giftId, string userId)
        {
            var winner = new Winner
            {
                GiftId = giftId,
                UserId = userId,
                WonAt = DateTime.UtcNow
            };
            _context.Winners.Add(winner);
            await _context.SaveChangesAsync();

            await _context.Entry(winner).Reference(w => w.Gift).LoadAsync();
            await _context.Entry(winner).Reference(w => w.User).LoadAsync();

            return new WinnerReadDTO
            {
                Id = winner.Id,
                GiftId = winner.GiftId,
                GiftName = winner.Gift?.Name ?? string.Empty,
                UserId = winner.UserId,
                UserName = winner.User?.UserName ?? string.Empty,
                UserEmail = winner.User?.Email ?? string.Empty,
                WonAt = winner.WonAt
            };
        }
    }
}