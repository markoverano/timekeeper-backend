using TimeKeeper.Core.DTO;

namespace TimeKeeper.Core.Interface.Services
{
    public interface IAttendanceService
    {
        Task<List<AttendanceEntryDto>> GetAllAttendancesAsync();
        Task<AttendanceEntryDto> GetAttendanceByIdAsync(int id);
        Task CreateAttendanceAsync(int employeeId);
        Task UpdateAttendanceAsync(int id, AttendanceEntryDto attendanceDto);
        Task TimeoutAsync(int employeeId);
        Task<List<AttendanceEntryDto>> GetEntriesByEmployeeIdAsync(int employeeId);
    }
}
