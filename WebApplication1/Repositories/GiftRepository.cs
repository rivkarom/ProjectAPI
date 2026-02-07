using ChineseAuctionProject.DTOs;
using ChineseAuctionProject.Interfaces;
using ChineseAuctionProject.Models;
using StoreApi.Data;
using Microsoft.EntityFrameworkCore;

namespace ChineseAuctionProject.Repositories
{
    public class GiftRepository : IGiftRepository
    {
        private readonly ApplicationDbContext _context;

        public GiftRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GiftDTOs.GiftReadDTO>> GetAllGiftsAsync()
        {
            var gifts = await _context.Gifts
                .Include(g => g.Category)
                .Include(g => g.Winners)
                    .ThenInclude(w => w.User)
                .Select(gift => new GiftDTOs.GiftReadDTO
                {
                    Id = gift.Id,
                    Name = gift.Name,
                    CategoryId = gift.CategoryId,
                    CategoryName = gift.Category != null ? gift.Category.Name : null,
                    Description = gift.Description,
                    WinnersCount = gift.WinnersCount,
                    TicketPrice = gift.TicketPrice,
                    ImageUrl = gift.ImageUrl,
                    DonorId = gift.DonorId,
                    IsRaffled = gift.IsRaffled,
                    RaffleDate = gift.RaffleDate,
                    Winners = gift.Winners.Select(w => new WinnerDTOs.WinnerReadDTO
                    {
                        Id = w.Id,
                        GiftId = w.GiftId,
                        GiftName = gift.Name,
                        UserId = w.UserId,
                        UserName = w.User != null ? w.User.UserName : string.Empty,
                        UserEmail = w.User != null ? w.User.Email : string.Empty,
                        WonAt = w.WonAt
                    }).ToList()
                })
                .ToListAsync();

            return gifts;
        }

        public async Task<GiftDTOs.GiftReadDTO?> GetGiftByIdAsync(int id)
        {
            var gift = await _context.Gifts
                .Include(g => g.Category)
                .Include(g => g.Winners)
                    .ThenInclude(w => w.User)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (gift == null)
            {
                return null;
            }

            return new GiftDTOs.GiftReadDTO
            {
                Id = gift.Id,
                Name = gift.Name,
                CategoryId = gift.CategoryId,
                CategoryName = gift.Category?.Name,
                Description = gift.Description,
                WinnersCount = gift.WinnersCount,
                TicketPrice = gift.TicketPrice,
                ImageUrl = gift.ImageUrl,
                DonorId = gift.DonorId,
                IsRaffled = gift.IsRaffled,
                RaffleDate = gift.RaffleDate,
                Winners = gift.Winners.Select(w => new WinnerDTOs.WinnerReadDTO
                {
                    Id = w.Id,
                    GiftId = w.GiftId,
                    GiftName = gift.Name,
                    UserId = w.UserId,
                    UserName = w.User != null ? w.User.UserName : string.Empty,
                    UserEmail = w.User != null ? w.User.Email : string.Empty,
                    WonAt = w.WonAt
                }).ToList()
            };
        }

        public async Task<IEnumerable<GiftDTOs.GiftReadDTO>> GetGiftsByCategoryAsync(int categoryId)
        {
            var gifts = await _context.Gifts
                .Where(g => g.CategoryId == categoryId)
                .Include(g => g.Category)
                .Include(g => g.Winners)
                    .ThenInclude(w => w.User)
                .Select(gift => new GiftDTOs.GiftReadDTO
                {
                    Id = gift.Id,
                    Name = gift.Name,
                    CategoryId = gift.CategoryId,
                    CategoryName = gift.Category != null ? gift.Category.Name : null,
                    Description = gift.Description,
                    WinnersCount = gift.WinnersCount,
                    TicketPrice = gift.TicketPrice,
                    DonorId = gift.DonorId,
                    IsRaffled = gift.IsRaffled,
                    RaffleDate = gift.RaffleDate,
                    Winners = gift.Winners.Select(w => new WinnerDTOs.WinnerReadDTO
                    {
                        Id = w.Id,
                        GiftId = w.GiftId,
                        GiftName = gift.Name,
                        UserId = w.UserId,
                        UserName = w.User != null ? w.User.UserName : string.Empty,
                        UserEmail = w.User != null ? w.User.Email : string.Empty,
                        WonAt = w.WonAt
                    }).ToList()
                })
                .ToListAsync();

            return gifts;
        }

        public async Task<GiftDTOs.GiftReadDTO> CreateGiftAsync(GiftDTOs.GiftCreateDTO createDto)
        {
            var gift = new Gift
            {
                Name = createDto.Name,
                CategoryId = createDto.CategoryId,
                Description = createDto.Description,
                WinnersCount = createDto.WinnersCount,
                TicketPrice = createDto.TicketPrice,
                ImageUrl = createDto.ImageUrl,
                DonorId = createDto.DonorId
            };
            _context.Gifts.Add(gift);
            await _context.SaveChangesAsync();

            await _context.Entry(gift).Reference(g => g.Category).LoadAsync();

            return new GiftDTOs.GiftReadDTO
            {
                Id = gift.Id,
                Name = gift.Name,
                CategoryId = gift.CategoryId,
                CategoryName = gift.Category?.Name,
                Description = gift.Description,
                WinnersCount = gift.WinnersCount,
                TicketPrice = gift.TicketPrice,
                ImageUrl = gift.ImageUrl,
                DonorId = gift.DonorId,
                IsRaffled = gift.IsRaffled,
                RaffleDate = gift.RaffleDate,
                Winners = new List<WinnerDTOs.WinnerReadDTO>()
            };
        }

        public async Task<GiftDTOs.GiftReadDTO?> UpdateGiftAsync(int id, GiftDTOs.GiftUpdateDTO updateDto)
        {
            var gift = await _context.Gifts.FindAsync(id);
            if (gift == null)
            {
                return null;
            }

            gift.Name = updateDto.Name;
            gift.CategoryId = updateDto.CategoryId;
            gift.Description = updateDto.Description;
            gift.WinnersCount = updateDto.WinnersCount;
            gift.TicketPrice = updateDto.TicketPrice;
            gift.ImageUrl = updateDto.ImageUrl;
            gift.DonorId = updateDto.DonorId;

            await _context.SaveChangesAsync();
            await _context.Entry(gift).Reference(g => g.Category).LoadAsync();

            return new GiftDTOs.GiftReadDTO
            {
                Id = gift.Id,
                Name = gift.Name,
                CategoryId = gift.CategoryId,
                CategoryName = gift.Category?.Name,
                Description = gift.Description,
                WinnersCount = gift.WinnersCount,
                TicketPrice = gift.TicketPrice,
                DonorId = gift.DonorId,
                IsRaffled = gift.IsRaffled,
                RaffleDate = gift.RaffleDate,
                Winners = new List<WinnerDTOs.WinnerReadDTO>()
            };
        }

        public async Task<bool> DeleteGiftAsync(int id)
        {
            var gift = await _context.Gifts.FindAsync(id);
            if (gift == null)
            {
                return false;
            }
            _context.Gifts.Remove(gift);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ConductRaffleAsync(int giftId)
        {
            var gift = await _context.Gifts.FindAsync(giftId);
            if (gift == null || gift.IsRaffled) return false;

            var orders = await _context.OrderManagements
                .Where(o => o.GiftId == giftId && o.IsPaid)
                .ToListAsync();

            if (!orders.Any()) return false;

            var participants = new List<string>();
            foreach (var order in orders)
            {
                for (int i = 0; i < order.TicketsCount; i++)
                {
                    participants.Add(order.UserId);
                }
            }

            if (participants.Count < gift.WinnersCount) return false; // not enough participants

            var random = new Random();
            var winners = participants.OrderBy(x => random.Next()).Distinct().Take(gift.WinnersCount).ToList();

            foreach (var winnerId in winners)
            {
                var winner = new Winner
                {
                    GiftId = giftId,
                    UserId = winnerId,
                    WonAt = DateTime.UtcNow
                };
                _context.Winners.Add(winner);
            }

            gift.IsRaffled = true;
            gift.RaffleDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}