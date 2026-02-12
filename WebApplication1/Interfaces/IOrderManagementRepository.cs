using static ChineseAuctionProject.DTOs.OrderManagementDTOs;

namespace ChineseAuctionProject.Interfaces
{
    public interface IOrderManagementRepository
    {
        Task<IEnumerable<OrderManagmentReadDto>> GetAllOrderManagementAsync();
        Task<OrderManagmentReadDto?> GetOrderManagementByIdAsync(int id);
        Task<OrderManagmentReadDto> CreateOrderManagementAsync(OrderManagmentCreateDto createDto);
        Task<OrderManagmentReadDto?> UpdateOrderManagementAsync(int id, OrderManagmentUpdateDto updateDto);
        Task<IEnumerable<OrderManagmentReadDto>> GetOrdersByGiftIdAsync(int giftId);
        //Task<LoginResponseDto?> AuthenticateAsync(string email, string password);
    }
}
