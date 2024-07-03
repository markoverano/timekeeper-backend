using TimeKeeper.Core.DTO;

namespace TimeKeeper.Core.Interface.Services
{
    public interface IEmployeeService
    {
        Task CreateEmployeeAsync(EmployeeDto employeeDto, string password);
        Task<EmployeeDto> GetEmployeeByIdAsync(int id);
        Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync();
    }
}
