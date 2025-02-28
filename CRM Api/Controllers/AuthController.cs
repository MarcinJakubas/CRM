using CRM_Api.Models;
using CRM_Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace CRM_Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly CRMDbContext _context;

        public AuthController(CRMDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == loginRequest.Username);
            if (user == null) return Unauthorized("Invalid username or password");

            // Weryfikacja hasła
            if (!VerifyPassword(loginRequest.Password, user.PasswordHash))
                return Unauthorized("Invalid username or password");

            return Ok(new { user.Id, user.Username});
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            using var sha256 = SHA256.Create();
            var hash = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(password)));
            return hash == storedHash;
        }
    }
}