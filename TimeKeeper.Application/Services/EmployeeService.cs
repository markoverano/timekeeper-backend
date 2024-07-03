using AutoMapper;
using TimeKeeper.Core.DTO;
using TimeKeeper.Core.Entities;
using TimeKeeper.Core.Interface.Repositories;
using TimeKeeper.Core.Interface.Services;
using static TimeKeeper.Infrastructure.Helpers.Utility;

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
            PasswordHasher hasher = new PasswordHasher();
            var (hashedPassword, salt) = hasher.HashPassword(password);

            var user = new UserDetail
            {
                UserId = Guid.NewGuid(),
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                Email = employeeDto.Email,
                PhoneNumber = employeeDto.PhoneNumber,
                RoleId = employeeDto.RoleId,
                PasswordHash = hashedPassword,
                Salt = Convert.ToBase64String(salt)
            };

            await _userDetailsRepository.AddUserAsync(user);

            var employee = new Employee
            {
                UserId = user.UserId
            };

            await _employeeRepository.AddEmployeeAsync(employee);
        }
        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync()
        {
            var employees = await _employeeRepository.GetAllEmployeesAsync();
            return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
        }

        public async Task<EmployeeDto> GetEmployeeByIdAsync(int id)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
            return _mapper.Map<EmployeeDto>(employee);
        }
    }
}
