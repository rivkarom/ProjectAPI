using static ChineseAuctionProject.DTOs.WinnerDTOs;

namespace ChineseAuctionProject.Interfaces
{
    public interface IWinnerService
    {
        Task<IEnumerable<WinnerReadDTO>> GetWinnersByGiftIdAsync(int giftId);
        Task<IEnumerable<WinnerReadDTO>> GetAllWinnersAsync();
        Task<WinnerReadDTO> CreateWinnerAsync(int giftId, string userId);
        Task<decimal> GetTotalRevenueAsync();
    }
}