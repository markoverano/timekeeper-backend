using TimeKeeper.Core.Entities;

namespace TimeKeeper.Core.Interface.Repositories
{
    public interface IAttendanceRepository
    {
        Task<List<AttendanceEntry>> GetAllEntriesAsync();
        Task<AttendanceEntry?> GetEntryByIdAsync(int id);
        Task AddEntryAsync(AttendanceEntry entry);
        Task UpdateEntryAsync(AttendanceEntry entry);
        Task<IEnumerable<AttendanceEntry>> GetAllEntriesByEmployeeAsync(int employeeId);
        Task<AttendanceEntry?> GetEntryByDateAndEmployeeIdAsync(DateTime today, int employeeId);
        Task<bool> HasTimeInEntryAsync(int employeeId);
        Task UpdateTimeOutAsync(int employeeId, DateTime timeOut);
    }
}
