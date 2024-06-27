using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeKeeper.Core.DTO;

namespace TimeKeeper.Core.Interface.Services
{
    public interface IAttendanceService
    {
        Task<List<AttendanceEntryDto>> GetAllAttendancesAsync();
        Task<AttendanceEntryDto> GetAttendanceByIdAsync(int id);
        Task CreateAttendanceAsync(int employeeId);
        Task UpdateAttendanceAsync(int id, AttendanceEntryDto attendanceDto);
        Task DeleteAttendanceAsync(int id);
    }
}
