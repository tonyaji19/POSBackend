using POSBackend.Models;
using POSBackend.Models.DTOs;

namespace POSBackend.Services.Interface
{
    public interface IAuthService
    {
        Task<string> Login(LoginRequest request);
        Task Register(RegisterRequest request);
    }
}
