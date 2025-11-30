using OtoMangaStore.Application.DTOs.Auth;

namespace OtoMangaStore.Application.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDto> LoginAdminAsync(LoginRequestDto request);
        Task<bool> LogoutAsync();
        Task<bool> IsUserInRoleAsync(string userId, string role);
        Task<List<string>> GetUserRolesAsync(string userId);
    }
}