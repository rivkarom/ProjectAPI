using ChineseAuctionProject.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static ChineseAuctionProject.DTOs.CartDTOs;

namespace ChineseAuctionProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "customer")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartReadDTO>>> GetCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var cart = await _cartService.GetCartByUserIdAsync(userId);
            return Ok(cart);
        }

        [HttpPost]
        public async Task<ActionResult<CartReadDTO>> AddToCart(CartCreateDTO createDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            createDto.UserId = userId;
            var item = await _cartService.AddToCartAsync(createDto);
            return CreatedAtAction(nameof(GetCart), item);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CartReadDTO>> UpdateCartItem(int id, CartUpdateDTO updateDto)
        {
            var item = await _cartService.UpdateCartItemAsync(id, updateDto);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var deleted = await _cartService.RemoveFromCartAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        [HttpPost("confirm")]
        public async Task<IActionResult> ConfirmCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var confirmed = await _cartService.ConfirmCartAsync(userId);
            if (!confirmed) return BadRequest("No items to confirm");
            return Ok();
        }
    }
}