using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ChineseAuctionProject.DTOs;
using ChineseAuctionProject.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ChineseAuctionProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public AuthController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await _userService.AuthenticateAsync(request.Email, request.Password);
            if (user == null) return Unauthorized("Invalid credentials");

            var token = GenerateToken(user);
            return Ok(token);
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var createDto = new UserDTOs.UserCreateDTO
            {
                Id = request.Id,
                UserName = request.UserName,
                Email = request.Email,
                Phone = request.Phone,
                Password = request.Password,
                IsAdmin = false
            };

            var created = await _userService.CreateUserAsync(createDto);
            var token = GenerateToken(new ChineseAuctionProject.Models.User
            {
                Id = created.Id,
                UserName = created.UserName,
                Email = created.Email,
                Phone = created.Phone,
                HashPassword = request.Password,
                IsAdmin = false
            });

            return CreatedAtAction(nameof(Login), new { email = created.Email }, token);
        }

        private AuthResponseDto GenerateToken(ChineseAuctionProject.Models.User user)
        {
            var jwtSection = _configuration.GetSection("Jwt");
            var key = jwtSection["Key"] ?? string.Empty;
            var issuer = jwtSection["Issuer"];
            var audience = jwtSection["Audience"];
            var expiresMinutes = int.TryParse(jwtSection["ExpiresMinutes"], out var minutes) ? minutes : 60;

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            if (user.IsAdmin)
            {
                claims.Add(new Claim(ClaimTypes.Role, "manager"));
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.Role, "customer"));
            }

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var creds = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(expiresMinutes);

            var tokenDescriptor = new JwtSecurityToken(
                issuer,
                audience,
                claims,
                expires: expires,
                signingCredentials: creds);

            var token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

            return new AuthResponseDto
            {
                Token = token,
                ExpiresAtUtc = expires,
                UserName = user.UserName,
                Email = user.Email,
                IsAdmin = user.IsAdmin
            };
        }
    }
}
