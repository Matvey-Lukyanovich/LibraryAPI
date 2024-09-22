using MediatR;
using Microsoft.AspNetCore.Mvc;
using LibraryAPI.Application.Auth.Commands;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace LibraryAPI.WebApi.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            try
            {
                var response = await _mediator.Send(command);
                return Ok(response);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
