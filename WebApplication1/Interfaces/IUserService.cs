using static ChineseAuctionProject.DTOs.DonorDTOs;
using static ChineseAuctionProject.DTOs.UserDTOs;

namespace ChineseAuctionProject.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponseDto>> GetAllUsersAsync();
        Task<UserResponseDto?> GetUserByIdAsync(string id);
        Task<UserResponseDto> CreateUserAsync(UserCreateDTO createDto);
        Task<UserResponseDto?> UpdateUserAsync(string id, UserUpdateDTO updateDto);
        Task<bool> DeleteUserAsync(string id);
        Task<ChineseAuctionProject.Models.User?> AuthenticateAsync(string email, string password);

    }
}
