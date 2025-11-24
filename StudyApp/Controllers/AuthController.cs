using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StudyApp.Data;
using StudyApp.DTO;
using StudyApp.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace StudyApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly StudyAppDbContext _db;
        private readonly IConfiguration _config;

        public AuthController(StudyAppDbContext db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (await _db.Users.AnyAsync(u => u.UserName == dto.UserName))
                return BadRequest("Username already taken");

            if (await _db.Users.AnyAsync(u => u.Email == dto.Email))
                return BadRequest("Email already exists.");

            var user = new User
            {
                UserName = dto.UserName,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            return Ok(new { message = "Successfully registered!" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.UserName == dto.UserName);
            if (user == null) return Unauthorized("Invalid credentials");

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return Unauthorized("Invalid credentials");

            var token = GenerateJwtToken(user);
            return Ok(new AuthResultDto { Token = token, UserId = user.Id, UserName = user.UserName, Role = user.Role });
        }

        private string GenerateJwtToken(User user)
        {
            var jwtSection = _config.GetSection("Jwt");
            var key = Encoding.UTF8.GetBytes(jwtSection["Key"]);
            var issuer = jwtSection["Issuer"];
            var audience = jwtSection["Audience"];
            var expiresMinutes = int.Parse(jwtSection["ExpiresMinutes"] ?? "60");

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Role, user.Role)
        };

            var creds = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer,
                audience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(expiresMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
