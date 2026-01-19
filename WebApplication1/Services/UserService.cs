using ChineseAuctionProject.Interfaces;
using static ChineseAuctionProject.DTOs.UserDTOs;

namespace ChineseAuctionProject.Services
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserService> _logger;
        public UserService(IUserService userService, ILogger<UserService> logger)
        {
            _userService = userService;
            _logger = logger;
        }
        public async Task<IEnumerable<UserResponseDto>> GetAllUsersAsync()
        {

        }
        public async Task<UserResponseDto?> GetUserByIdAsync(int id)
        {

        }
        public async Task<UserResponseDto> CreateUserAsync(UserCreateDTO createDto)
        {

        }
        public async Task<UserResponseDto?> UpdateUserAsync(int id, UserUpdateDTO updateDto)
        {

        }
        public async Task<bool> DeleteUserAsync(int id)
        {

        }
    }
}
