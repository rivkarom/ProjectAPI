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
                .Include(o => o.Gift)
                .Include(o => o.User)
                .Select(order => new OrderManagmentReadDto
                {
                    GiftId = order.GiftId,
                    TicketsCount = order.TicketsCount,
                    IsPaid = order.IsPaid,
                    GiftName = order.Gift != null ? order.Gift.Name : string.Empty,
                    TicketPrice = order.Gift != null ? order.Gift.TicketPrice : 0,
                    UserName = order.User != null ? order.User.UserName : string.Empty
                })
                .ToListAsync();
        }

        public async Task<OrderManagmentReadDto?> GetOrderManagementByIdAsync(int id)
        {
            return await _context.OrderManagements
                 .Where(order => order.Id == id)
                 .Include(o => o.Gift)
                 .Include(o => o.User)
                 .Select(order => new OrderManagmentReadDto
                 {
                     GiftId = order.GiftId,
                     TicketsCount = order.TicketsCount,
                     IsPaid = order.IsPaid,
                     GiftName = order.Gift != null ? order.Gift.Name : string.Empty,
                     TicketPrice = order.Gift != null ? order.Gift.TicketPrice : 0,
                     UserName = order.User != null ? order.User.UserName : string.Empty
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

            await _context.Entry(order).Reference(o => o.Gift).LoadAsync();
            await _context.Entry(order).Reference(o => o.User).LoadAsync();

            return new OrderManagmentReadDto
            {
                GiftId = order.GiftId,
                TicketsCount = order.TicketsCount,
                IsPaid = order.IsPaid,
                GiftName = order.Gift?.Name ?? string.Empty,
                TicketPrice = order.Gift?.TicketPrice ?? 0,
                UserName = order.User != null ? order.User.UserName : string.Empty
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
            await _context.Entry(order).Reference(o => o.Gift).LoadAsync();
            await _context.Entry(order).Reference(o => o.User).LoadAsync();
            return new OrderManagmentReadDto
            {
                GiftId = order.GiftId,
                TicketsCount = order.TicketsCount,
                IsPaid = order.IsPaid,
                GiftName = order.Gift?.Name ?? string.Empty,
                TicketPrice = order.Gift?.TicketPrice ?? 0,
                UserName = order.User != null ? order.User.UserName : string.Empty
            };
        }

        public async Task<IEnumerable<OrderManagmentReadDto>> GetOrdersByGiftIdAsync(int giftId)
        {
            return await _context.OrderManagements
                .Where(order => order.GiftId == giftId)
                .Include(o => o.Gift)
                .Include(o => o.User)
                .Select(order => new OrderManagmentReadDto
                {
                    GiftId = order.GiftId,
                    TicketsCount = order.TicketsCount,
                    IsPaid = order.IsPaid,
                    GiftName = order.Gift != null ? order.Gift.Name : string.Empty,
                    TicketPrice = order.Gift != null ? order.Gift.TicketPrice : 0,
                    UserName = order.User != null ? order.User.UserName : string.Empty
                })
                .ToListAsync();
        }
    }
}