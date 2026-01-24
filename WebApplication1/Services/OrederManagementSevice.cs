using ChineseAuctionProject.Interfaces;
using static ChineseAuctionProject.DTOs.OrderManagementDTOs;

namespace ChineseAuctionProject.Services
{
    public class OrderManagementService : IOrderManagmentService
    {
        private readonly IOrderManagementRepository _orderManagementRepository;
        private readonly ILogger<OrderManagementService> _logger;

        public OrderManagementService(IOrderManagementRepository orderManagementRepository, ILogger<OrderManagementService> logger)
        {
            _orderManagementRepository = orderManagementRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<OrderManagmentReadDto>> GetAllOrderManagementAsync()
        {
            return await _orderManagementRepository.GetAllOrderManagementAsync();
        }

        public async Task<OrderManagmentReadDto?> GetOrderManagementByIdAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("GetOrderManagementByIdAsync called with invalid id: {Id}", id);
                return null;
            }
            return await _orderManagementRepository.GetOrderManagementByIdAsync(id);
        }

        public async Task<OrderManagmentReadDto> CreateOrderManagementAsync(OrderManagmentCreateDto createDto)
        {
            if (createDto == null)
            {
                throw new ArgumentNullException(nameof(createDto));
            }
            return await _orderManagementRepository.CreateOrderManagementAsync(createDto);
        }

        public async Task<OrderManagmentReadDto?> UpdateOrderManagementAsync(int id, OrderManagmentUpdateDto updateDto)
        {
            if (updateDto == null || id <= 0)
            {
                return null;
            }
            return await _orderManagementRepository.UpdateOrderManagementAsync(id, updateDto);
        }
    }
}