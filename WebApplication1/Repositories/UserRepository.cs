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
            var user = new Models.User
            {
                UserName = createDto.UserName,
                Email = createDto.Email,
                PasswordHash = createDto.PasswordHash
            };
            _context.Users.Add(user);
            _context.SaveChanges();
            return Task.FromResult(new UserDTOs.UserResponseDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email
            });
        }

        Task<bool> IUserRepository.DeleteUserAsync(string id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return Task.FromResult(false);
            }
            _context.Users.Remove(user);
            _context.SaveChanges();
            return Task.FromResult(true);
        }

        Task<IEnumerable<UserDTOs.UserResponseDto>> IUserRepository.GetAllUsersAsync()
        {
            var users = _context.Users
                .Select(user => new UserDTOs.UserResponseDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email
                })
                .ToList();
            return Task.FromResult((IEnumerable<UserDTOs.UserResponseDto>)users);
        }

        Task<UserDTOs.UserResponseDto?> IUserRepository.GetUserByIdAsync(int id)
        {
            if (id.ToString() == null)
            {
                return Task.FromResult<UserDTOs.UserResponseDto?>(null);
            }
            var user = _context.Users.Find(id.ToString());
            if (user == null)
            {
                return Task.FromResult<UserDTOs.UserResponseDto?>(null);
            }
            var userDto = new UserDTOs.UserResponseDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email
            };
            return Task.FromResult<UserDTOs.UserResponseDto?>(userDto);
        }

        Task<UserDTOs.UserResponseDto?> IUserRepository.UpdateUserAsync(string id, UserDTOs.UserUpdateDTO updateDto)
        {
           if (id == null)
            {
                return Task.FromResult<UserDTOs.UserResponseDto?>(null);
            }
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return Task.FromResult<UserDTOs.UserResponseDto?>(null);
            }
            // Update fields if they are provided in the updateDto
            if (!string.IsNullOrEmpty(updateDto.UserName))
            {
                user.UserName = updateDto.UserName;
            }
            if (!string.IsNullOrEmpty(updateDto.Email))
            {
                user.Email = updateDto.Email;
            }
            if (!string.IsNullOrEmpty(updateDto.PasswordHash))
            {
                user.PasswordHash = updateDto.PasswordHash;
            }
            _context.SaveChanges();
            var userDto = new UserDTOs.UserResponseDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email
            };
            return Task.FromResult<UserDTOs.UserResponseDto?>(userDto);

        }
    }
}