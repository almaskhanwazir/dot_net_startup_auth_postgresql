using LearningPlatformBackend.Data;
using LearningPlatformBackend.Entities;
using LearningPlatformBackend.Models;
using LearningPlatformBackend.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LearningPlatformBackend.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AuthService(ApplicationDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _passwordHasher = new PasswordHasher<User>();

        }

        public async Task<string> AuthenticateAsync(AuthRequest authRequest)
        {

            var user = await _dbContext.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Username == authRequest.Username);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid username.");
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, authRequest.Password);

            var isValid = result == PasswordVerificationResult.Success;

            if (!isValid)
            {
                throw new UnauthorizedAccessException("Invalid Password.");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role.Name)
                }),
                Expires = DateTime.UtcNow.AddHours(12),//expires in 12 hours
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
