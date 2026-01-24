using ChineseAuctionProject.Interfaces;
using ChineseAuctionProject.Models;
using StoreApi.Data;
using static ChineseAuctionProject.DTOs.OrderManagementDTOs;
using Microsoft.EntityFrameworkCore;

namespace ChineseAuctionProject.Repositories
{
    public class OrderManagementRepository : IOrderManagementRepository
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
                IsPaid = false
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

            if (!string.IsNullOrEmpty(updateDto.UserId))
            {
                order.UserId = updateDto.UserId;
            }

            if (updateDto.GiftId.HasValue && updateDto.GiftId.Value > 0)
            {
                order.GiftId = updateDto.GiftId.Value;
            }

            if (updateDto.TicketsCount.HasValue && updateDto.TicketsCount.Value > 0)
            {
                order.TicketsCount = updateDto.TicketsCount.Value;
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
    }
}