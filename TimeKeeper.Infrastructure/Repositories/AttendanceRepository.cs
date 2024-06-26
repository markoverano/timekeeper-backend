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

        public async Task<IEnumerable<AttendanceEntry>> GetAllEntriesAsync()
        {
            return await _context.AttendanceEntries.ToListAsync();
        }

        public async Task<AttendanceEntry> GetEntryByIdAsync(int id)
        {
            return await _context.AttendanceEntries.FindAsync(id);
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

        public async Task DeleteEntryAsync(int id)
        {
            var entry = await _context.AttendanceEntries.FindAsync(id);
            if (entry != null)
            {
                _context.AttendanceEntries.Remove(entry);
                await _context.SaveChangesAsync();
            }
        }
    }
}