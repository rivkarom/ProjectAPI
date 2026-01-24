using ChineseAuctionProject.Interfaces;
using ChineseAuctionProject.Models;
using ChineseAuctionProject.DTOs;

namespace ChineseAuctionProject.Services
{
    public class DonorService : IDonorService
    {
        private readonly IDonorRepository _donorRepository;
        private readonly ILogger<DonorService> _logger;

        public DonorService(IDonorRepository donorRepository, ILogger<DonorService> logger)
        {
            _donorRepository = donorRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<DonorDTOs.DonorResponseDto>> GetAllDonorsAsync()
        {
            return await _donorRepository.GetAllDonorsAsync();
        }

        public async Task<DonorDTOs.DonorResponseDto?> GetDonorByIdAsync(int id)
        {
            return await _donorRepository.GetDonorByIdAsync(id);
        }

        public async Task<DonorDTOs.DonorResponseDto> CreateDonorAsync(DonorDTOs.DonorCreateDto createDto)
        {
            return await _donorRepository.CreateDonorAsync(createDto);
        }

        public async Task<DonorDTOs.DonorResponseDto?> UpdateDonorAsync(int id, DonorDTOs.DonorUpdateDto updateDto)
        {
            return await _donorRepository.UpdateDonorAsync(id, updateDto);
        }
    }
}