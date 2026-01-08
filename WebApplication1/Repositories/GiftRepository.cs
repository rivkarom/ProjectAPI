using ChineseAuctionProject.DTOs;
using ChineseAuctionProject.Interfaces;
using ChineseAuctionProject.Models;
using Microsoft.EntityFrameworkCore;
using StoreApi.Data;

namespace ChineseAuctionProject.Repositories
{
    public class GiftRepository: IGiftRepository
    {

        private readonly ApplicationDbContext _context;

        public GiftRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<GiftDTOs.GiftReadDTO>> GetAllGiftsAsync()
        {
            return await _context.Gifts
                .Select(gift => new GiftDTOs.GiftReadDTO
                {
                    Id = gift.Id,
                    Name = gift.Name,
                    Category = gift.Category,
                    Description = gift.Description,
                    WinnersCount = gift.WinnersCount,
                    TicketPrice = gift.TicketPrice,
                    WinnersList = gift.WinnersList,
                    DonorId = gift.DonorId
                })
                .ToListAsync();
        }
        public async Task<GiftDTOs.GiftReadDTO?> GetGiftByIdAsync(int id)//יחזיר נל אם לא קיים
        {             var gift = await _context.Gifts.FindAsync(id);
            if (gift == null)
            {
                return null;
            }
            return new GiftDTOs.GiftReadDTO
            {
                Id = gift.Id,
                Name = gift.Name,
                Category = gift.Category,
                Description = gift.Description,
                WinnersCount = gift.WinnersCount,
                TicketPrice = gift.TicketPrice,
                WinnersList = gift.WinnersList,
                DonorId = gift.DonorId
            };
        }
        public async Task<GiftDTOs.GiftReadDTO> CreateGiftAsync(GiftDTOs.GiftCreateDTO createDto)
        {
            var gift =new Gift
             {
                Name = createDto.Name,
                Category = createDto.Category,
                Description = createDto.Description,
                WinnersCount = createDto.WinnersCount,
                TicketPrice = createDto.TicketPrice,
                DonorId = createDto.DonorId
            };
            _context.Gifts.Add(gift);
            await _context.SaveChangesAsync();
            return new GiftDTOs.GiftReadDTO
            {
                Id = gift.Id,
                Name = gift.Name,
                Category = gift.Category,
                Description = gift.Description,
                WinnersCount = gift.WinnersCount,
                TicketPrice = gift.TicketPrice,
                WinnersList = gift.WinnersList,
                DonorId = gift.DonorId
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
            gift.Category = updateDto.Category;
            gift.Description = updateDto.Description;
            gift.WinnersCount = updateDto.WinnersCount;
            gift.TicketPrice = updateDto.TicketPrice;
            gift.DonorId = updateDto.DonorId;
            await _context.SaveChangesAsync();
            return new GiftDTOs.GiftReadDTO
            {
                Id = gift.Id,
                Name = gift.Name,
                Category = gift.Category,
                Description = gift.Description,
                WinnersCount = gift.WinnersCount,
                TicketPrice = gift.TicketPrice,
                WinnersList = gift.WinnersList,
                DonorId = gift.DonorId
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
    }
}
