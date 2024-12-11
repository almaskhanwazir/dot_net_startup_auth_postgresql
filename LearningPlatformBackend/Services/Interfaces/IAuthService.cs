using LearningPlatformBackend.Models;

namespace LearningPlatformBackend.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> AuthenticateAsync(AuthRequest authRequest);
    }
}
