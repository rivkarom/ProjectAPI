using ChineseAuctionProject.Interfaces;
using StoreApi.Data;
using static ChineseAuctionProject.DTOs.OrderManagementDTOs;
using ChineseAuctionProject.Models;
using Microsoft.EntityFrameworkCore;



namespace ChineseAuctionProject.Repositories
{
    public class OrderManagementRepository: IOrderManagementRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderManagementRepository(ApplicationDbContext context)
        {
            _context = context;
        }

    public async Task<IEnumerable<OrderManagmentReadDto>> GetAllOrderManagementAsync()
        {
            return await _context.OrderManagements
                .Select(order => new OrderManagmentReadDto
                {
                    GiftId = order.GiftId,
                    TicketsCount = order.TicketsCount,
                    IsPaid = order.IsPaid
                })
                .ToListAsync();
        }
        
        public async Task<OrderManagmentReadDto?> GetOrderManagementByIdAsync(int id)
        {
           return await _context.OrderManagements
                .Where(order => order.Id == id)
                .Select(order => new OrderManagmentReadDto
                {
                    GiftId = order.GiftId,
                    TicketsCount = order.TicketsCount,
                    IsPaid = order.IsPaid
                })
                .FirstOrDefaultAsync();
        }
        public async Task<OrderManagmentReadDto> CreateOrderManagementAsync(OrderManagmentCreateDto createDto)
        {
            var order = new OrderManagement
            {
                UserId = createDto.UserId,
                GiftId = createDto.GiftId,
                TicketsCount = createDto.TicketsCount,
                IsPaid = false // Default value for new orders
            };
            _context.OrderManagements.Add(order);
            await _context.SaveChangesAsync();  
            return new OrderManagmentReadDto
            {
                GiftId = order.GiftId,
                TicketsCount = order.TicketsCount,
                IsPaid = order.IsPaid
            };
        }
        public async Task<OrderManagmentReadDto?> UpdateOrderManagementAsync(int id, OrderManagmentUpdateDto updateDto)
        {
            var order = await _context.OrderManagements.FindAsync(id);
            if (order == null)
            {
                return null;
            }
            // Update fields if they are provided in the updateDto
            if (!string.IsNullOrEmpty(updateDto.UserId))
            {
                order.UserId = updateDto.UserId;
            }
            if (updateDto.GiftId != 0)
            {
                order.GiftId = updateDto.GiftId;
            }
            if (updateDto.TicketsCount != 0)
            {
                order.TicketsCount = updateDto.TicketsCount;
            }
            order.IsPaid = updateDto.IsPaid;
            await _context.SaveChangesAsync();
            return new OrderManagmentReadDto
            {
                GiftId = order.GiftId,
                TicketsCount = order.TicketsCount,
                IsPaid = order.IsPaid
            };
        }
        //public async Task<LoginResponseDto?> AuthenticateAsync(string email, string password)
        //{
        //    OrderManagement? order = await _context.OrderManagements
        //        .FirstOrDefaultAsync(o => o.UserId == email && o.Password == password);
        //}

    }
}
