using AutoMapper;
using Moq;
using TimeKeeper.Application.DtoMapper;
using TimeKeeper.Application.Services;
using TimeKeeper.Core.DTO;
using TimeKeeper.Core.Entities;
using TimeKeeper.Core.Interface.Repositories;
using Xunit;

namespace Application.Tests
{
    public class EmployeeServiceTests
    {
        private readonly IMapper _mapper;

        public EmployeeServiceTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AttendanceMapperProfile>();
            });
            _mapper = configuration.CreateMapper();
        }

        [Fact]
        public async Task CreateEmployeeAsync_ValidEmployee_CreatesEmployee()
        {
            var employeeDto = new EmployeeDto
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "1234567890",
                RoleId = 1
            };

            var mockEmployeeRepository = new Mock<IEmployeeRepository>();
            var mockUserDetailsRepository = new Mock<IUserDetailsRepository>();

            var employeeService = new EmployeeService(mockEmployeeRepository.Object, mockUserDetailsRepository.Object, _mapper);

            // Act
            await employeeService.CreateEmployeeAsync(employeeDto, "password");

            // Assert
            mockEmployeeRepository.Verify(repo => repo.AddEmployeeAsync(It.IsAny<Employee>()), Times.Once);
            mockUserDetailsRepository.Verify(repo => repo.AddUserAsync(It.IsAny<UserDetail>()), Times.Once);
        }
    }
}
