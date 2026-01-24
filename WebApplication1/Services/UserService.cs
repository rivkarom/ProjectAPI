using ChineseAuctionProject.Interfaces;
using static ChineseAuctionProject.DTOs.UserDTOs;

namespace ChineseAuctionProject.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, ILogger<UserService> logger, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<IEnumerable<UserResponseDto>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<UserResponseDto?> GetUserByIdAsync(string id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }

        public async Task<UserResponseDto> CreateUserAsync(UserCreateDTO createDto)
        {
            return await _userRepository.CreateUserAsync(createDto);
        }

        public async Task<UserResponseDto?> UpdateUserAsync(string id, UserUpdateDTO updateDto)
        {
            return await _userRepository.UpdateUserAsync(id, updateDto);
        }

        public async Task<bool> DeleteUserAsync(string id)
        {
            return await _userRepository.DeleteUserAsync(id);
        }
    }
}