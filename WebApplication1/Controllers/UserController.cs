using ChineseAuctionProject.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static ChineseAuctionProject.DTOs.UserDTOs;

namespace ChineseAuctionProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "manager")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetAll()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponseDto>> GetById(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<UserResponseDto>> Create(UserCreateDTO createDto)
        {
            var user = await _userService.CreateUserAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserResponseDto>> Update(string id, UserUpdateDTO updateDto)
        {
            var updated = await _userService.UpdateUserAsync(id, updateDto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var removed = await _userService.DeleteUserAsync(id);
            if (!removed) return NotFound();
            return NoContent();
        }
    }
}
