using ChineseAuctionProject.DTOs;
using ChineseAuctionProject.Interfaces;

namespace ChineseAuctionProject.Services;
    public class GiftService:IGiftService
    {
        private readonly IGiftRepository _giftRepository;
        private readonly ILogger<GiftService> _logger;

        public GiftService(IGiftRepository giftRepository, ILogger<GiftService> logger)
        {
            _giftRepository = giftRepository;
            _logger = logger;
        }
        public async Task<IEnumerable<GiftDTOs.GiftReadDTO>> GetAllGiftsAsync()
        {
            return await _giftRepository.GetAllGiftsAsync();
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
    }

