using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using LibraryAPI.WebApi.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using LibraryAPI.Domain;
using Microsoft.EntityFrameworkCore;
using LibraryAPI.Persistence;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using LibraryAPI.WebApi.Models;
using System.Threading.Tasks;

namespace LibraryAPI.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly TokenService _tokenService;
        private readonly LibraryDbContext _context;

        public AuthController(TokenService tokenService, LibraryDbContext context)
        {
            _tokenService = tokenService;
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.UserName == request.UserName && u.Password == request.Password);

            if (user == null)
            {
                return Unauthorized();
            }

            var token = _tokenService.GenerateToken(user.Id.ToString(), user.Role);
            return Ok(new { Token = token });
        }

        // Метод для валидации пользователя (здесь можно добавить свою логику)
        private User ValidateUser(string username, string password)
        {
            // Примерная логика, замените своей
            // Загрузка пользователя из базы данных или другой источник
            return new User { Id = 1, Role = "User" }; // Пример
        }

    }
}
