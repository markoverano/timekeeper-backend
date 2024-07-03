using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TimeKeeper.Application.Services;
using TimeKeeper.Core.Entities;
using TimeKeeper.Infrastructure.Data;
using Xunit;
using static TimeKeeper.Infrastructure.Helpers.Utility;

namespace Application.Tests
{
    public class AuthServiceTests
    {
        private readonly IConfiguration _configuration;
        private readonly DbContextOptions<ApplicationDbContext> _options;

        public AuthServiceTests()
        {
            _configuration = new ConfigurationBuilder()
                         .AddInMemoryCollection(new Dictionary<string, string>
                         {
                            { "Jwt:Key", "nGkrYZ19QUvkgbg7psuuoQOrPt6hjJ8s" },
                            { "Jwt:Issuer", "8{m)|*,zW<E*tv=" },
                            { "Jwt:Audience", "fEUC8Hkl>UX5c=s" },
                            { "Jwt:ExpireMinutes", "30" }
                         }).Build();

            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "AuthServiceTests")
                .Options;

            SeedTestData();
        }

        private void SeedTestData()
        {
            PasswordHasher hasher = new PasswordHasher();

            using (var context = new ApplicationDbContext(_options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var (hashedPassword, salt) = hasher.HashPassword("admin123");

                if (!context.UserRoles.Any())
                {
                    context.UserRoles.AddRange(
                        new UserRole { RoleId = 1, RoleName = "Admin" },
                        new UserRole { RoleId = 2, RoleName = "Staff" }
                    );
                }

                if (!context.Permissions.Any())
                {
                    context.Permissions.AddRange(
                        new Permission { Id = 1, Name = "CanEdit" },
                        new Permission { Id = 2, Name = "CanExportPDF" }
                    );
                }

                if (!context.RolePermissions.Any())
                {
                    context.RolePermissions.AddRange(
                        new RolePermission { RoleId = 1, PermissionId = 1 },
                        new RolePermission { RoleId = 1, PermissionId = 2 }
                    );
                }

                if (!context.UserDetails.Any())
                {
                    context.UserDetails.Add(new UserDetail
                    {
                        UserId = Guid.NewGuid(),
                        FirstName = "Admin",
                        LastName = "User",
                        Email = "admin@user.com",
                        PhoneNumber = "1234567890",
                        RoleId = 1,
                        PasswordHash = hashedPassword,
                        Salt = Convert.ToBase64String(salt)
                    });
                }

                context.SaveChanges();
            }
        }


        [Fact]
        public async Task Authenticate_ValidCredentials_ReturnsLoginResponse()
        {
            // Arrange
            using (var context = new ApplicationDbContext(_options))
            {
                var authService = new AuthService(_configuration, context);

                // Verify Data Seeding
                var userDetails = context.UserDetails.ToList();
                Assert.NotEmpty(userDetails);
                Assert.NotNull(userDetails.FirstOrDefault(u => u.Email == "admin@user.com"));

                // Act
                var result = await authService.Authenticate("admin@user.com", "admin123");

                // Assert
                Assert.NotNull(result);
                Assert.NotEmpty(result.Token);
                Assert.Equal("Admin", result.UserRole);
                Assert.Equal(2, result.Permissions.Count);
                Assert.Equal(1, result.EmployeeId);
            }
        }

        [Fact]
        public async Task Authenticate_InvalidCredentials_ReturnsNull()
        {
            // Arrange
            using (var context = new ApplicationDbContext(_options))
            {
                var authService = new AuthService(_configuration, context);

                // Act
                var result = await authService.Authenticate("admin@user.com", "invalidpassword");

                // Assert
                Assert.Null(result);
            }
        }

        [Fact]
        public async Task Authenticate_UserNotFound_ReturnsNull()
        {
            // Arrange
            using (var context = new ApplicationDbContext(_options))
            {
                var authService = new AuthService(_configuration, context);

                // Act
                var result = await authService.Authenticate("nonexistent@user.com", "password");

                // Assert
                Assert.Null(result);
            }
        }
    }
}