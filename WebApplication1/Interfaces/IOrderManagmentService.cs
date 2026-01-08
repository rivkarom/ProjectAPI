using static ChineseAuctionProject.DTOs.OrderManegementDTOs;
using static ChineseAuctionProject.DTOs.UserDTOs;

namespace ChineseAuctionProject.Interfaces
{
    public interface IOrderManagmentService
    {
        Task<IEnumerable<UserResponseDto>> GetAllOrderManagementAsync();
        Task<UserResponseDto?> GetOrderManagementByIdAsync(int id);
        Task<UserResponseDto> CreateOrderManagementAsync(OrderManagmentCreateDto createDto);
        Task<UserResponseDto?> UpdateOrderManagementAsync(int id, OrderManagmentUpdateDto updateDto);
        //Task<LoginResponseDto?> AuthenticateAsync(string email, string password);

    }
}
