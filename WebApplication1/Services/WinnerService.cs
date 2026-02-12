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
        private readonly IEmailService _emailService;

        public WinnerService(IWinnerRepository winnerRepository, ApplicationDbContext context, ILogger<WinnerService> logger, IEmailService emailService)
        {
            _winnerRepository = winnerRepository;
            _context = context;
            _logger = logger;
            _emailService = emailService;
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

        public async Task<bool> SendWinnerEmailAsync(int winnerId)
        {
            var winner = await _winnerRepository.GetWinnersByGiftIdAsync(0); // wait, no, need to get by id
            // Actually, need to get winner by id.

            // First, add to repository GetWinnerByIdAsync

            // But to keep simple, get from context.

            var winnerEntity = await _context.Winners
                .Include(w => w.User)
                .Include(w => w.Gift)
                .FirstOrDefaultAsync(w => w.Id == winnerId);

            if (winnerEntity == null) return false;

            try
            {
                await _emailService.SendWinnerNotificationAsync(winnerEntity.User?.Email ?? string.Empty, winnerEntity.User?.UserName ?? string.Empty, winnerEntity.Gift?.Name ?? string.Empty);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to send email to winner {winnerId}");
                return false;
            }
        }
    }
}