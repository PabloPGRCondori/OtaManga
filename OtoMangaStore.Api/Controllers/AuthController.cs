using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using OtoMangaStore.Application.DTOs.Auth;
using MediatR;
using OtoMangaStore.Application.UseCases.Auth.Commands.Login;
using OtoMangaStore.Application.UseCases.Auth.Commands.Logout;
using System.Linq;
using System.Threading.Tasks;

namespace OtoMangaStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,Editor")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("admin/login")]
        [AllowAnonymous]
        public async Task<IActionResult> AdminLogin([FromBody] OtoMangaStore.Api.DTOs.Requests.LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new LoginResponseDto
                {
                    Success = false,
                    Message = "Datos inválidos"
                });
            }

            var command = new LoginCommand
            {
                Email = request.Email,
                Password = request.Password,
                RememberMe = request.RememberMe
            };

            var result = await _mediator.Send(command);
            
            if (!result.Success)
            {
                return Unauthorized(result);
            }

            return Ok(result);
        }

        [HttpPost("logout")]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            var result = await _mediator.Send(new LogoutCommand());
            
            if (result)
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return Ok(new { success = true, message = "Sesión cerrada correctamente" });
            }

            return BadRequest(new { success = false, message = "Error al cerrar sesión" });
        }

        [HttpGet("check-auth")]
        [AllowAnonymous]
        public IActionResult CheckAuth()
        {
            return Ok(new
            {
                authenticated = true,
                user = User.Identity?.Name,
                roles = User.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.Role).Select(c => c.Value)
            });
        }
    }
}