using ChineseAuctionProject.DTOs;
using ChineseAuctionProject.Interfaces;
using StoreApi.Data;

namespace ChineseAuctionProject.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        Task<UserDTOs.UserResponseDto> IUserRepository.CreateUserAsync(UserDTOs.UserCreateDTO createDto)
        {
            throw new NotImplementedException();
        }

        Task<bool> IUserRepository.DeleteUserAsync(string id)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<UserDTOs.UserResponseDto>> IUserRepository.GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        Task<UserDTOs.UserResponseDto?> IUserRepository.GetUserByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<UserDTOs.UserResponseDto?> IUserRepository.UpdateUserAsync(string id, UserDTOs.UserUpdateDTO updateDto)
        {
            throw new NotImplementedException();
        }
    }
}