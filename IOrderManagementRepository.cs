csharp WebApplication1\Interfaces\IOrderManagementRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using static ChineseAuctionProject.DTOs.OrderManagementDTOs;

namespace ChineseAuctionProject.Interfaces
{
    public interface IOrderManagementRepository
    {
        Task<IEnumerable<OrderManagementReadDto>> GetAllOrderManagementAsync();
        Task<OrderManagementReadDto?> GetOrderManagementByIdAsync(int id);
        Task<OrderManagementReadDto> CreateOrderManagementAsync(OrderManagementCreateDto createDto);
        Task<OrderManagementReadDto?> UpdateOrderManagementAsync(int id, OrderManagementUpdateDto updateDto);
        Task<bool> DeleteOrderManagementAsync(int id);
    }
}