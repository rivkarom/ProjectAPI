using ChineseAuctionProject.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static ChineseAuctionProject.DTOs.WinnerDTOs;

namespace ChineseAuctionProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "manager")]
    public class WinnerController : ControllerBase
    {
        private readonly IWinnerService _winnerService;

        public WinnerController(IWinnerService winnerService)
        {
            _winnerService = winnerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WinnerReadDTO>>> GetAll()
        {
            var winners = await _winnerService.GetAllWinnersAsync();
            return Ok(winners);
        }

        [HttpGet("gift/{giftId}")]
        public async Task<ActionResult<IEnumerable<WinnerReadDTO>>> GetByGiftId(int giftId)
        {
            var winners = await _winnerService.GetWinnersByGiftIdAsync(giftId);
            return Ok(winners);
        }

        [HttpGet("revenue")]
        public async Task<ActionResult<decimal>> GetTotalRevenue()
        {
            var revenue = await _winnerService.GetTotalRevenueAsync();
            return Ok(revenue);
        }

        [HttpPost("{id}/send-email")]
        public async Task<IActionResult> SendWinnerEmail(int id)
        {
            var success = await _winnerService.SendWinnerEmailAsync(id);
            if (!success) return NotFound();
            return Ok();
        }
    }
}