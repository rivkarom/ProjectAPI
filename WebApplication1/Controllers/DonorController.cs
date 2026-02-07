using ChineseAuctionProject.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static ChineseAuctionProject.DTOs.DonorDTOs;

namespace ChineseAuctionProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "manager")]
    public class DonorController : ControllerBase
    {
        private readonly IDonorService _donorService;

        public DonorController(IDonorService donorService)
        {
            _donorService = donorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DonorResponseDto>>> GetAll([FromQuery] string? filterBy)
        {
            var donors = await _donorService.GetAllDonorsAsync();

            if (!string.IsNullOrEmpty(filterBy))
            {
                donors = donors.Where(d => d.Name.Contains(filterBy, StringComparison.OrdinalIgnoreCase) ||
                                           d.Email.Contains(filterBy, StringComparison.OrdinalIgnoreCase) ||
                                           d.Donations.Any(g => g.Name.Contains(filterBy, StringComparison.OrdinalIgnoreCase)));
            }

            return Ok(donors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DonorResponseDto>> GetById(int id)
        {
            var donor = await _donorService.GetDonorByIdAsync(id);
            if (donor == null) return NotFound();
            return Ok(donor);
        }

        [HttpPost]
        public async Task<ActionResult<DonorResponseDto>> Create(DonorCreateDto createDto)
        {
            var donor = await _donorService.CreateDonorAsync(createDto);
            return Ok(donor);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DonorResponseDto>> Update(int id, DonorUpdateDto updateDto)
        {
            var donor = await _donorService.UpdateDonorAsync(id, updateDto);
            if (donor == null) return NotFound();
            return Ok(donor);
        }
    }
}
