using TimeKeeper.Core.Entities;

namespace TimeKeeper.Core.Interface.Repositories
{
    public interface IEmployeeRepository
    {
        Task AddEmployeeAsync(Employee employee);
        Task<Employee> GetEmployeeByIdAsync(int id);
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
    }
}
