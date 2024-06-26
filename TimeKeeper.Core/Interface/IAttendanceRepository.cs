using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeKeeper.Core.Interface
{
    public interface IAttendanceRepository
    {
        Task<IEnumerable<Entities.AttendanceEntry>> GetAllEntriesAsync();
        Task<Entities.AttendanceEntry> GetEntryByIdAsync(int id);
        Task AddEntryAsync(Entities.AttendanceEntry entry);
        Task UpdateEntryAsync(Entities.AttendanceEntry entry);
        Task DeleteEntryAsync(int id);
    }
}
