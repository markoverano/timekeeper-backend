using AutoMapper;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using TimeKeeper.Core.DTO;
using TimeKeeper.Core.Entities;
using TimeKeeper.Core.Interface.Repositories;
using TimeKeeper.Core.Interface.Services;

namespace TimeKeeper.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUserDetailsRepository _userDetailsRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IUserDetailsRepository userDetailsRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _userDetailsRepository = userDetailsRepository;
            _mapper = mapper;
        }

        public async Task CreateEmployeeAsync(EmployeeDto employeeDto, string password)
        {
            var user = new UserDetail
            {
                UserId = Guid.NewGuid(),
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                Email = employeeDto.Email,
                PhoneNumber = employeeDto.PhoneNumber,
                RoleId = employeeDto.RoleId,
                PasswordHash = HashPassword(password)
            };

            await _userDetailsRepository.AddUserAsync(user);

            var employee = new Employee
            {
                UserId = user.UserId
            };

            await _employeeRepository.AddEmployeeAsync(employee);
        }

        public async Task<EmployeeDto> GetEmployeeByIdAsync(int id)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
            return _mapper.Map<EmployeeDto>(employee);
        }

        private string HashPassword(string password)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashed;
        }
    }
}
