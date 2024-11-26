using Microsoft.EntityFrameworkCore;
using POSBackend.Data;
using POSBackend.Helpers;
using POSBackend.Models;
using POSBackend.Models.DTOs;
using POSBackend.Services.Interface;

namespace POSBackend.Services.Impl
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<string> Login(LoginRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                throw new Exception("Invalid credentials");

            var jwtKey = _configuration["Jwt:Key"];
            var jwtIssuer = _configuration["Jwt:Issuer"];
            var jwtAudience = _configuration["Jwt:Audience"];
            var jwtExpirationMinutes = int.Parse(_configuration["Jwt:ExpirationMinutes"]);

            return JwtHelper.GenerateJwtToken(user.Username, jwtKey, jwtIssuer, jwtAudience, jwtExpirationMinutes);
        }

        public async Task Register(RegisterRequest request)
        {
            if (await _context.Users.AnyAsync(u => u.Username == request.Username))
                throw new Exception("Username already exists");

            var user = new User
            {
                Username = request.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
    }
}
