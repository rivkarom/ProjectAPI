using ChineseAuctionProject.Interfaces;
using static ChineseAuctionProject.DTOs.OrderManagementDTOs;
using static ChineseAuctionProject.DTOs.UserDTOs;

namespace ChineseAuctionProject.Services
{
    public class OrderManagementService:IOrderManagmentService
    {
        private readonly IOrderManagementRepository _orderManagementRepository;
        private readonly ILogger<OrderManagementService> _logger;
        public OrderManagementService(IOrderManagementRepository orderManagementRepository, ILogger<OrderManagementService> logger)
        {
            _orderManagementRepository = orderManagementRepository;
            _logger = logger;
        }
        public async Task<IEnumerable<UserResponseDto>> GetAllOrderManagementAsync()
        {

        }
        public async Task<UserResponseDto?> GetOrderManagementByIdAsync(int id)
        {

        }
        public async Task<UserResponseDto> CreateOrderManagementAsync(OrderManagmentCreateDto createDto)
        {

        }
        public async Task<UserResponseDto?> UpdateOrderManagementAsync(int id, OrderManagmentUpdateDto updateDto)
        {

        }
        //public async Task<LoginResponseDto?> AuthenticateAsync(string email, string password){

    }
}
