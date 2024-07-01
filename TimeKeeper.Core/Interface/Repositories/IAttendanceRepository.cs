using TimeKeeper.Core.Entities;

namespace TimeKeeper.Core.Interface.Repositories
{
    public interface IAttendanceRepository
    {
        Task<List<AttendanceEntry>> GetAllEntriesAsync();
        Task<AttendanceEntry?> GetEntryByIdAsync(int id);
        Task AddEntryAsync(AttendanceEntry entry);
        Task UpdateEntryAsync(AttendanceEntry entry);
        Task DeleteEntryAsync(int id);
        Task<IEnumerable<AttendanceEntry>> GetAllEntriesByEmployeeAsync(int employeeId);
        Task<AttendanceEntry?> GetEntryByDateAndEmployeeIdAsync(DateTime today, int employeeId);
    }
}
