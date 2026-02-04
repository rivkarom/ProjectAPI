using ChineseAuctionProject.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static ChineseAuctionProject.DTOs.OrderManagementDTOs;

namespace ChineseAuctionProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "manager")]
    public class OrderManagementController : ControllerBase
    {
        private readonly IOrderManagmentService _orderService;

        public OrderManagementController(IOrderManagmentService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderManagmentReadDto>>> GetAll()
        {
            var orders = await _orderService.GetAllOrderManagementAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderManagmentReadDto>> GetById(int id)
        {
            var order = await _orderService.GetOrderManagementByIdAsync(id);
            if (order == null) return NotFound();
            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<OrderManagmentReadDto>> Create(OrderManagmentCreateDto createDto)
        {
            var order = await _orderService.CreateOrderManagementAsync(createDto);
            return Ok(order);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OrderManagmentReadDto>> Update(int id, OrderManagmentUpdateDto updateDto)
        {
            var order = await _orderService.UpdateOrderManagementAsync(id, updateDto);
            if (order == null) return NotFound();
            return Ok(order);
        }
    }
}
