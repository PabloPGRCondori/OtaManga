using MediatR;
using Microsoft.AspNetCore.Identity;
using OtoMangaStore.Domain.Models;

namespace OtoMangaStore.Application.UseCases.Auth.Commands.Logout
{
    public class LogoutHandler : IRequestHandler<LogoutCommand, bool>
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LogoutHandler(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<bool> Handle(LogoutCommand request, CancellationToken cancellationToken)
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
    }
}
