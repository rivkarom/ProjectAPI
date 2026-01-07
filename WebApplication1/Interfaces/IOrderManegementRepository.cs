using static ChineseAuctionProject.DTOs.OrderManegementDTOs;

namespace ChineseAuctionProject.Interfaces
{
    public interface IOrderManegementRepository
    {
        Task<IEnumerable<OrderManagmentReadDto>> GetAllOrderManagementAsync();
        Task<OrderManagmentReadDto?> GetOrderManagementByIdAsync(int id);
        Task<OrderManagmentReadDto> CreateOrderManagementAsync(OrderManagmentCreateDto createDto);
        Task<OrderManagmentReadDto?> UpdateOrderManagementAsync(int id, OrderManagmentUpdateDto updateDto);
        //Task<LoginResponseDto?> AuthenticateAsync(string email, string password);
    }
}
