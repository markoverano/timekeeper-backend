using Microsoft.EntityFrameworkCore;
using TimeKeeper.Core.Entities;
using TimeKeeper.Core.Interface.Repositories;
using TimeKeeper.Infrastructure.Data;

namespace TimeKeeper.Infrastructure.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly ApplicationDbContext _context;

        public AttendanceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<AttendanceEntry>> GetAllEntriesAsync()
        {
            return await _context.AttendanceEntries.AsNoTracking().OrderByDescending(x=>x.Date).ToListAsync();
        }

        public async Task<IEnumerable<AttendanceEntry>> GetAllEntriesByEmployeeAsync(int employeeId)
        {
            return await _context.AttendanceEntries
                .Where(x => x.EmployeeId == employeeId)
                .AsNoTracking().OrderByDescending(x=>x.Date)
                .ToListAsync();
        }

        public async Task<AttendanceEntry?> GetEntryByIdAsync(int id)
        {
            return await _context.AttendanceEntries.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddEntryAsync(AttendanceEntry entry)
        {
            await _context.AttendanceEntries.AddAsync(entry);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEntryAsync(AttendanceEntry entry)
        {
            _context.AttendanceEntries.Update(entry);
            await _context.SaveChangesAsync();
        }

        public async Task<AttendanceEntry?> GetEntryByDateAndEmployeeIdAsync(DateTime today, int employeeId)
        {
            return await _context.AttendanceEntries
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.EmployeeId == employeeId && x.Date == today.Date);
        }

        public async Task<bool> HasTimeInEntryAsync(int employeeId)
        {
            return await _context.AttendanceEntries
                .AnyAsync(a => a.EmployeeId == employeeId &&
                a.Date == DateTime.Today &&
                a.TimeIn != null &&
                a.TimeOut == null);
        }

        public async Task UpdateTimeOutAsync(int employeeId, DateTime timeOut)
        {
            var entry = await _context.AttendanceEntries
                .FirstOrDefaultAsync(
                    a => a.EmployeeId == employeeId && 
                    a.Date == timeOut.Date && 
                    a.TimeIn != null && 
                    a.TimeOut == null);

            if (entry != null)
            {
                entry.TimeOut = timeOut;
                await _context.SaveChangesAsync();
            }
        }
    }
}