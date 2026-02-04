using ChineseAuctionProject.DTOs;
using ChineseAuctionProject.Interfaces;
using StoreApi.Data;
using static ChineseAuctionProject.DTOs.UserDTOs;
using static ChineseAuctionProject.Models.User;
using Microsoft.EntityFrameworkCore;

namespace ChineseAuctionProject.Repositories
{


public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<UserResponseDto>> GetAllUsersAsync()
        {
            return await _context.Users
                .Select(user => new UserResponseDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Phone = user.Phone
                })
                .ToListAsync();
        }
        public async Task<UserResponseDto?> GetUserByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id)) return null;

            var user = await _context.Users.FindAsync(id);
            if (user == null) return null;

            return new UserResponseDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Phone = user.Phone
            };
        }

        public async Task<Models.User?> GetByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return null;
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task<UserResponseDto> CreateUserAsync(UserCreateDTO createDto)
        {
            if (createDto == null)
            {
                throw new ArgumentNullException(nameof(createDto));
            }
            var user = new Models.User
            {
                Id = createDto.Id,
                UserName = createDto.UserName,
                Email = createDto.Email,
                Phone = createDto.Phone,
                HashPassword = createDto.Password,
                IsAdmin = createDto.IsAdmin
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return new UserResponseDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Phone = user.Phone
            };

        }
        public async Task<UserResponseDto?> UpdateUserAsync(string id, UserUpdateDTO updateDto)
        {
            if (updateDto == null || id != updateDto.Id)
            {
                return null;
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return null;
            }
            user.UserName = updateDto.UserName;
            user.Email = updateDto.Email;
            user.Phone = updateDto.Phone;
            user.IsAdmin = updateDto.IsAdmin;
            await _context.SaveChangesAsync();

            return new UserResponseDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Phone = user.Phone
            };
        }
        public async Task<bool> DeleteUserAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return false;
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return false;
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}