using Microsoft.AspNetCore.Identity;
using OtoMangaStore.Application.DTOs.Auth;
using OtoMangaStore.Application.Interfaces;
using OtoMangaStore.Domain.Models;

namespace OtoMangaStore.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthService(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<LoginResponseDto> LoginAdminAsync(LoginRequestDto request)
        {
            var response = new LoginResponseDto();

            try
            {
                // Buscar usuario por email
                var user = await _userManager.FindByEmailAsync(request.Email);
                
                if (user == null)
                {
                    response.Success = false;
                    response.Message = "Credenciales incorrectas";
                    return response;
                }

                // Verificar que el usuario tenga rol Admin o Editor
                var roles = await _userManager.GetRolesAsync(user);
                bool hasPermission = roles.Contains("Admin") || roles.Contains("Editor");

                if (!hasPermission)
                {
                    response.Success = false;
                    response.Message = "No tienes permisos para acceder al panel administrativo";
                    return response;
                }

                // Intentar login
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
                        FullName = user.UserName ?? user.Email ?? "Usuario", // ✅ CORREGIDO
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

        public async Task<bool> LogoutAsync()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> IsUserInRoleAsync(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            return await _userManager.IsInRoleAsync(user, role);
        }

        public async Task<List<string>> GetUserRolesAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return new List<string>();

            var roles = await _userManager.GetRolesAsync(user);
            return roles.ToList();
        }
    }
}