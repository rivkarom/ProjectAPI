using static ChineseAuctionProject.DTOs.DonorDTOs;

namespace ChineseAuctionProject.Interfaces
{
    public interface IDonorRepository
    {
            Task<IEnumerable<DonorResponseDto>> GetAllDonorsAsync();
            Task<DonorResponseDto?> GetDonorByIdAsync(int id);
            Task<DonorResponseDto> CreateDonorAsync(DonorCreateDto createDto);
            Task<DonorResponseDto?> UpdateDonorAsync(int id, DonorUpdateDto updateDto);
            //Task<LoginResponseDto?> AuthenticateAsync(string email, string password);

        
    }
}
