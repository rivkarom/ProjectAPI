using ChineseAuctionProject.DTOs;
using ChineseAuctionProject.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChineseAuctionProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GiftController : ControllerBase
    {
        private readonly IGiftService _giftService;

        public GiftController(IGiftService giftService)
        {
            _giftService = giftService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GiftDTOs.GiftReadDTO>>> GetAll([FromQuery] string? sortBy, [FromQuery] string? search)
        {
            var gifts = await _giftService.GetAllGiftsAsync();

            if (!string.IsNullOrEmpty(search))
            {
                gifts = gifts.Where(g => g.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                                         g.DonorId?.Contains(search) == true);
            }

            if (!string.IsNullOrEmpty(sortBy))
            {
                gifts = sortBy.ToLower() switch
                {
                    "price" => gifts.OrderBy(g => g.TicketPrice),
                    "category" => gifts.OrderBy(g => g.CategoryName),
                    _ => gifts
                };
            }

            return Ok(gifts);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<GiftDTOs.GiftReadDTO>> GetById(int id)
        {
            var gift = await _giftService.GetGiftByIdAsync(id);
            if (gift == null) return NotFound();
            return Ok(gift);
        }

        [AllowAnonymous]
        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<GiftDTOs.GiftReadDTO>>> GetByCategory(int categoryId)
        {
            var gifts = await _giftService.GetGiftsByCategoryAsync(categoryId);
            return Ok(gifts);
        }

        [Authorize(Roles = "manager")]
        [HttpPost]
        public async Task<ActionResult<GiftDTOs.GiftReadDTO>> Create(GiftDTOs.GiftCreateDTO createDto)
        {
            var gift = await _giftService.CreateGiftAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = gift.Id }, gift);
        }

        [Authorize(Roles = "manager")]
        [HttpPut("{id}")]
        public async Task<ActionResult<GiftDTOs.GiftReadDTO>> Update(int id, GiftDTOs.GiftUpdateDTO updateDto)
        {
            var gift = await _giftService.UpdateGiftAsync(id, updateDto);
            if (gift == null) return NotFound();
            return Ok(gift);
        }

        [Authorize(Roles = "manager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _giftService.DeleteGiftAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        [Authorize(Roles = "manager")]
        [HttpPost("{id}/raffle")]
        public async Task<ActionResult<IEnumerable<WinnerDTOs.WinnerReadDTO>>> ConductRaffle(int id)
        {
            var winners = await _giftService.ConductRaffleAsync(id);
            return Ok(winners);
        }
    }
}
