using MediatR;
using Microsoft.AspNetCore.Identity;
using OtoMangaStore.Application.DTOs.Auth;
using OtoMangaStore.Domain.Models;

namespace OtoMangaStore.Application.UseCases.Auth.Commands.Login
{
    public class LoginHandler : IRequestHandler<LoginCommand, LoginResponseDto>
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public LoginHandler(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<LoginResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var response = new LoginResponseDto();

            try
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                
                if (user == null)
                {
                    response.Success = false;
                    response.Message = "Credenciales incorrectas";
                    return response;
                }

                var roles = await _userManager.GetRolesAsync(user);
                bool hasPermission = roles.Contains("Admin") || roles.Contains("Editor");

                if (!hasPermission)
                {
                    response.Success = false;
                    response.Message = "No tienes permisos para acceder al panel administrativo";
                    return response;
                }

                var result = await _signInManager.PasswordSignInAsync(
                    user.UserName ?? user.Email,
                    request.Password,
                    request.RememberMe,
                    lockoutOnFailure: true);

                if (result.Succeeded)
                {
                    response.Success = true;
                    response.Message = "Inicio de sesión exitoso";
                    response.RedirectUrl = "/Admin/Dashboard/Index";
                    response.UserInfo = new UserInfoDto
                    {
                        Id = user.Id,
                        Email = user.Email ?? "",
                        FullName = user.UserName ?? user.Email ?? "Usuario",
                        Roles = roles.ToList()
                    };
                }
                else if (result.IsLockedOut)
                {
                    response.Success = false;
                    response.Message = "Cuenta bloqueada. Intenta nuevamente más tarde";
                }
                else if (result.IsNotAllowed)
                {
                    response.Success = false;
                    response.Message = "Inicio de sesión no permitido. Verifica tu cuenta";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Credenciales incorrectas";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error en el servidor: {ex.Message}";
            }

            return response;
        }
    }
}
