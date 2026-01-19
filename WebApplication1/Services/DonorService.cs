using ChineseAuctionProject.Interfaces;
using ChineseAuctionProject.Models;
using static ChineseAuctionProject.DTOs.DonorDTOs;

namespace ChineseAuctionProject.Services
{
    public class DonorService:IDonorService
    {
        private readonly IDonorRepository _donorRepository;
        private readonly ILogger<DonorService> _logger;

        public DonorService(IDonorRepository donorRepository, ILogger<DonorService> logger)
        {
            _donorRepository = donorRepository;
            _logger = logger;
        }
        public async Task<IEnumerable<DonorResponseDto>> GetAllDonorsAsync()
        {
            var donors = await _donorRepository.GetAllDonorsAsync();
            return donors.Select(MapToResponseDto);
        }
        public async Task<DonorResponseDto?> GetDonorByIdAsync(int id)
        {
            return await _donorRepository.GetDonorByIdAsync(id);
        }
        public async Task<DonorResponseDto> CreateDonorAsync(DonorCreateDto createDto)
        {
            return await _donorRepository.CreateDonorAsync(createDto);
        }
        public async Task<DonorResponseDto?> UpdateDonorAsync(int id, DonorUpdateDto updateDto)
        {
            return await _donorRepository.UpdateDonorAsync(id, updateDto);
        }
        private static DonorResponseDto MapToResponseDto(Donor donor)
        {
            return new DonorResponseDto
            {
                Id = donor.Id,
                Name = donor.Name,
                Email = donor.Email,
                Phone = donor.Phone,
            };
        }
    }
}
