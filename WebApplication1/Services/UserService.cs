using ChineseAuctionProject.Interfaces;
using static ChineseAuctionProject.DTOs.UserDTOs;
using Microsoft.AspNetCore.Identity;

namespace ChineseAuctionProject.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserService> _logger;
        private readonly IPasswordHasher<ChineseAuctionProject.Models.User> _passwordHasher;

        public UserService(IUserRepository userRepository, ILogger<UserService> logger, IConfiguration configuration, IPasswordHasher<ChineseAuctionProject.Models.User> passwordHasher)
        {
            _userRepository = userRepository;
            _logger = logger;
            _configuration = configuration;
            _passwordHasher = passwordHasher;
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
            var hashedPassword = _passwordHasher.HashPassword(new ChineseAuctionProject.Models.User(), createDto.Password);
            createDto.Password = hashedPassword;
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

        public async Task<ChineseAuctionProject.Models.User?> AuthenticateAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null) return null;

            var result = _passwordHasher.VerifyHashedPassword(user, user.HashPassword, password);
            if (result == PasswordVerificationResult.Success) return user;
            return null;
        }
    }
}