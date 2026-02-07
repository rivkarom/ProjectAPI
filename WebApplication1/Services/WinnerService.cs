using ChineseAuctionProject.Interfaces;
using StoreApi.Data;
using Microsoft.EntityFrameworkCore;
using static ChineseAuctionProject.DTOs.WinnerDTOs;

namespace ChineseAuctionProject.Services
{
    public class WinnerService : IWinnerService
    {
        private readonly IWinnerRepository _winnerRepository;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<WinnerService> _logger;

        public WinnerService(IWinnerRepository winnerRepository, ApplicationDbContext context, ILogger<WinnerService> logger)
        {
            _winnerRepository = winnerRepository;
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<WinnerReadDTO>> GetWinnersByGiftIdAsync(int giftId)
        {
            return await _winnerRepository.GetWinnersByGiftIdAsync(giftId);
        }

        public async Task<IEnumerable<WinnerReadDTO>> GetAllWinnersAsync()
        {
            return await _winnerRepository.GetAllWinnersAsync();
        }

        public async Task<WinnerReadDTO> CreateWinnerAsync(int giftId, string userId)
        {
            return await _winnerRepository.CreateWinnerAsync(giftId, userId);
        }

        public async Task<decimal> GetTotalRevenueAsync()
        {
            return await _context.OrderManagements
                .Where(o => o.IsPaid)
                .Include(o => o.Gift)
                .SumAsync(o => o.TicketsCount * (o.Gift != null ? o.Gift.TicketPrice : 0m));
        }
    }
}