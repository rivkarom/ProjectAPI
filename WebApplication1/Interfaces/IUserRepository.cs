using static ChineseAuctionProject.DTOs.UserDTOs;

namespace ChineseAuctionProject.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserResponseDto>> GetAllUsersAsync();
        Task<UserResponseDto?> GetUserByIdAsync(string id);
        Task<ChineseAuctionProject.Models.User?> GetByEmailAsync(string email);
        Task<UserResponseDto> CreateUserAsync(UserCreateDTO createDto);
        Task<UserResponseDto?> UpdateUserAsync(string id, UserUpdateDTO updateDto);
        Task<bool> DeleteUserAsync(string id);
    }
}
