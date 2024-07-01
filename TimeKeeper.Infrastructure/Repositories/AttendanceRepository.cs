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
            return await _context.AttendanceEntries.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<AttendanceEntry>> GetAllEntriesByEmployeeAsync(int employeeId)
        {
            return await _context.AttendanceEntries
                .Where(x => x.EmployeeId == employeeId)
                .AsNoTracking()
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
            var employeeAttendance = await _context.AttendanceEntries
                                    .OrderByDescending(x => x.TimeIn)
                                    .FirstOrDefaultAsync(x => x.EmployeeId == entry.EmployeeId);

            if (employeeAttendance != null && employeeAttendance.TimeIn == null)
            {
                throw new Exception("You have no time in record for today.");
            }

            _context.AttendanceEntries.Update(entry);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEntryAsync(int id)
        {
            var entry = await _context.AttendanceEntries.FindAsync(id);
            if (entry != null)
            {
                _context.AttendanceEntries.Remove(entry);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<AttendanceEntry?> GetEntryByDateAndEmployeeIdAsync(DateTime today, int employeeId)
        {
            return await _context.AttendanceEntries
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.EmployeeId == employeeId && x.Date == today);
        }
    }
}