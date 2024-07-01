using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TimeKeeper.Core.DTO;
using TimeKeeper.Core.Entities;
using TimeKeeper.Core.Interface.Services;
using TimeKeeper.Infrastructure.Data;
using static TimeKeeper.Infrastructure.Helpers.Utility;

namespace TimeKeeper.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        private readonly PasswordHasher _passwordHasher;

        public AuthService(IConfiguration configuration, ApplicationDbContext context)
        {
            _configuration = configuration;
            _context = context;
            _passwordHasher = new PasswordHasher();
        }

        public async Task<LoginResponse> Authenticate(string email, string password)
        {
            var user = await _context.UserDetails
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null) return null;

            var salt = Convert.FromBase64String(user.Salt);
            var hashedInputPassword = _passwordHasher.HashPasswordWithSalt(password, salt);

            if (hashedInputPassword != user.PasswordHash) return null;

            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.UserId == user.UserId);

            var rolePermissions = await _context.RolePermissions
                .Where(rp => rp.RoleId == user.RoleId)
                .Include(rp => rp.Permission)
                .ToListAsync();

            var token = GenerateJwtToken(user);

            return new LoginResponse
            {
                Token = token,
                UserId = user.UserId,
                EmployeeId = employee?.Id ?? 0,
                Permissions = rolePermissions.Select(rp => rp.Permission.Name).ToList(),
                UserRole = user.Role.RoleName
            };
        }

        private string GenerateJwtToken(UserDetail user)
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                        new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    };
                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(double.Parse(_configuration["Jwt:ExpireMinutes"])),
                    signingCredentials: credentials);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}