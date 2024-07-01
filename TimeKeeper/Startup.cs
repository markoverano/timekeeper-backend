using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TimeKeeper.Application.Services;
using TimeKeeper.Core.Entities;
using TimeKeeper.Core.Interface.Repositories;
using TimeKeeper.Core.Interface.Services;
using TimeKeeper.Infrastructure.Data;
using TimeKeeper.Infrastructure.Repositories;

namespace TimeKeeper
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<UserDetail, IdentityRole>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidAudience = _configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]))
                };
            });

            services.AddSwaggerGen();

            services.AddScoped<IAttendanceService, AttendanceService>();
            services.AddScoped<IAttendanceRepository, AttendanceRepository>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IUserDetailsRepository, UserDetailsRepository>();

            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                          p => p.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod());
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseCors("AllowAll");

            app.UseRouting();

            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TimeKeeper API"));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
