using Microsoft.AspNetCore.Mvc;
using TimeKeeper.Core.DTO;
using TimeKeeper.Core.Interface.Services;

namespace TimeKeeper.Controllers
{
    [ApiController]
    [Route("api/attendances")]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;

        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        [HttpGet]
        public async Task<ActionResult<List<AttendanceEntryDto>>> GetAllAttendances()
        {
            var attendances = await _attendanceService.GetAllAttendancesAsync();
            return Ok(attendances);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AttendanceEntryDto>> GetAttendanceById(int id)
        {
            var attendance = await _attendanceService.GetAttendanceByIdAsync(id);
            if (attendance == null)
            {
                return NotFound();
            }
            return Ok(attendance);
        }

        [HttpGet("{employeeId}/entries")]
        public async Task<ActionResult<List<AttendanceEntryDto>>> GetEntriesByEmployeeIdAsync(int employeeId)
        {
            var attendance = await _attendanceService.GetEntriesByEmployeeIdAsync(employeeId);
            if (attendance == null)
            {
                return NotFound();
            }
            return Ok(attendance);
        }

        [HttpPost("{employeeId}")]
        public async Task<IActionResult> CreateAttendance(int employeeId)
        {
            await _attendanceService.CreateAttendanceAsync(employeeId);
            var attendanceDto = await _attendanceService.GetAttendanceByIdAsync(employeeId);
            return CreatedAtAction(nameof(GetAttendanceById), new { id = attendanceDto.Id }, attendanceDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAttendance(int id, AttendanceEntryDto attendanceDto)
        {
            await _attendanceService.UpdateAttendanceAsync(id, attendanceDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttendance(int id)
        {
            await _attendanceService.DeleteAttendanceAsync(id);
            return NoContent();
        }
    }
}
