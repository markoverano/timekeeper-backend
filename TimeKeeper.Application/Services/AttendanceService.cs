using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeKeeper.Core.DTO;
using TimeKeeper.Core.Entities;
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

        public async Task<List<AttendanceEntryDto>> GetAllAttendancesAsync()
        {
            var attendances = await _attendanceRepository.GetAllEntriesAsync();
            return MapAttendancesToDTOs(attendances.ToList());
        }

        public async Task<AttendanceEntryDto> GetAttendanceByIdAsync(int id)
        {
            var attendance = await _attendanceRepository.GetEntryByIdAsync(id);
            return MapAttendanceToDTO(attendance);
        }

        public async Task CreateAttendanceAsync(int employeeId)
        {
            //todo create catch for duplicate timein entry

            //var today = DateTime.Today;

            //var existingEntry = await _context.AttendanceEntries
            //    .FirstOrDefaultAsync(e => e.UserId == userId && e.Date == today && e.TimeOut == null);

            //if (existingEntry != null)
            //{
            //    throw Exception("Time-in entry already exists for today.");
            //}

            var attendanceEntry = new AttendanceEntryDto
            {
                EmployeeId = employeeId,
                TimeIn = DateTime.UtcNow,
                Date = DateTime.UtcNow
            };

            var mapped = MapDTOToAttendance(attendanceEntry);

            await _attendanceRepository.AddEntryAsync(mapped);
        }

        public async Task UpdateAttendanceAsync(int id, AttendanceEntryDto attendanceDto)
        {
            var existingAttendance = await _attendanceRepository.GetEntryByIdAsync(id);
            if (existingAttendance == null)
            {
                return;
            }

            existingAttendance.TimeIn = UpdateTimeComponent(existingAttendance.TimeIn, attendanceDto.TimeInString);
            existingAttendance.TimeOut = UpdateTimeComponent(existingAttendance.TimeOut, attendanceDto.TimeOutString);

            await _attendanceRepository.UpdateEntryAsync(existingAttendance);
        }

        private DateTime? UpdateTimeComponent(DateTime? originalDateTime, string timeString)
        {
            if (originalDateTime.HasValue && DateTime.TryParse(timeString, out DateTime parsedTime))
            {
                return new DateTime(originalDateTime.Value.Year, 
                                    originalDateTime.Value.Month, 
                                    originalDateTime.Value.Day,
                                    parsedTime.Hour, parsedTime.Minute, 
                                    parsedTime.Second);
            }
            return originalDateTime;
        }

        public async Task DeleteAttendanceAsync(int id)
        {
            var attendance = await _attendanceRepository.GetEntryByIdAsync(id);
            if (attendance == null)
            {
                return;
            }

            await _attendanceRepository.DeleteEntryAsync(id);
        }

        private AttendanceEntryDto MapAttendanceToDTO(AttendanceEntry attendance)
        {
            return new AttendanceEntryDto
            {
                Id = attendance.Id,
                Date = attendance.Date,
                TimeIn = attendance.TimeIn,
                TimeOut = attendance.TimeOut
            };
        }

        private List<AttendanceEntryDto> MapAttendancesToDTOs(List<AttendanceEntry> attendances)
        {
            var attendanceDTOs = new List<AttendanceEntryDto>();
            foreach (var attendance in attendances)
            {
                attendanceDTOs.Add(MapAttendanceToDTO(attendance));
            }
            return attendanceDTOs;
        }

        private AttendanceEntry MapDTOToAttendance(AttendanceEntryDto attendanceDto)
        {
            return new AttendanceEntry
            {
                Id = attendanceDto.Id,
                EmployeeId = attendanceDto.EmployeeId,
                Date = attendanceDto.Date,
                TimeIn = attendanceDto.TimeIn,
                TimeOut = attendanceDto.TimeOut
            };
        }
    }
}
