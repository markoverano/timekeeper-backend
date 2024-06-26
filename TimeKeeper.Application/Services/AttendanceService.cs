using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeKeeper.Core.DTO;
using TimeKeeper.Core.Interface.Repositories;
using TimeKeeper.Core.Interface.Services;

namespace TimeKeeper.Application.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _attendanceRepository;

        public AttendanceService(IAttendanceRepository attendanceRepository)
        {
            _attendanceRepository = attendanceRepository;
        }

        public Task CreateAttendanceAsync(AttendanceEntryDto attendanceDto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAttendanceAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<AttendanceEntryDto>> GetAllAttendancesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<AttendanceEntryDto> GetAttendanceByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAttendanceAsync(int id, AttendanceEntryDto attendanceDto)
        {
            throw new NotImplementedException();
        }
    }
}
