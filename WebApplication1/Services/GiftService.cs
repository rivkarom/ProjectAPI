using ChineseAuctionProject.DTOs;
using ChineseAuctionProject.Interfaces;
using System.Linq;
using static ChineseAuctionProject.DTOs.WinnerDTOs;

namespace ChineseAuctionProject.Services;
    public class GiftService:IGiftService
    {
        private readonly IGiftRepository _giftRepository;
        private readonly IWinnerRepository _winnerRepository;
        private readonly IEmailService _emailService;
        private readonly ILogger<GiftService> _logger;

        public GiftService(IGiftRepository giftRepository, IWinnerRepository winnerRepository, IEmailService emailService, ILogger<GiftService> logger)
        {
            _giftRepository = giftRepository;
            _winnerRepository = winnerRepository;
            _emailService = emailService;
            _logger = logger;
        }
        public async Task<IEnumerable<GiftDTOs.GiftReadDTO>> GetAllGiftsAsync()
        {
            return await _giftRepository.GetAllGiftsAsync();
        }
        public async Task<IEnumerable<GiftDTOs.GiftReadDTO>> GetGiftsByCategoryAsync(int categoryId)
        {
            if (categoryId <= 0)
            {
                return Enumerable.Empty<GiftDTOs.GiftReadDTO>();
            }

            return await _giftRepository.GetGiftsByCategoryAsync(categoryId);
        }
        public async Task<GiftDTOs.GiftReadDTO?> GetGiftByIdAsync(int id)//יחזיר נל אם לא קיים
        {
            if(id <= 0) 
                return null;
            var gift = await _giftRepository.GetGiftByIdAsync(id);
            if (gift == null)
                return null; 
            return gift;
        }
        public async Task<GiftDTOs.GiftReadDTO> CreateGiftAsync(DTOs.GiftDTOs.GiftCreateDTO createDto)
        {
            if(createDto == null)
                throw new ArgumentNullException(nameof(createDto));
            return await _giftRepository.CreateGiftAsync(createDto);


    }
        public async Task<GiftDTOs.GiftReadDTO?> UpdateGiftAsync(int id, DTOs.GiftDTOs.GiftUpdateDTO updateDto)
        {
            return await _giftRepository.UpdateGiftAsync(id, updateDto);
        }
        public async Task<bool> DeleteGiftAsync(int id)
        {
            return await _giftRepository.DeleteGiftAsync(id);
        }

        public async Task<IEnumerable<WinnerReadDTO>> ConductRaffleAsync(int giftId)
        {
            var success = await _giftRepository.ConductRaffleAsync(giftId);
            if (!success) return Enumerable.Empty<WinnerReadDTO>();

            var winners = await _winnerRepository.GetWinnersByGiftIdAsync(giftId);

            foreach (var winner in winners)
            {
                try
                {
                    await _emailService.SendWinnerNotificationAsync(winner.UserEmail, winner.UserName, winner.GiftName);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Failed to send email to {winner.UserEmail}");
                }
            }

            return winners;
        }
    }

