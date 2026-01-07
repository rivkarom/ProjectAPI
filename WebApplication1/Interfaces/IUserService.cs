using static ChineseAuctionProject.DTOs.DonorDTOs;
using static ChineseAuctionProject.DTOs.UserDTOs;

namespace ChineseAuctionProject.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponseDto>> GetAllUsersAsync();
        Task<UserResponseDto?> GetUserByIdAsync(int id);
        Task<UserResponseDto> CreateUserAsync(UserCreateDTO createDto);
        Task<UserResponseDto?> UpdateUserAsync(int id, UserUpdateDTO updateDto);
        Task<bool> DeleteUserAsync(int id);

    }
}
