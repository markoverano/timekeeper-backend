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

        [HttpPost]
        public async Task<IActionResult> CreateAttendance(AttendanceEntryDto attendanceDto)
        {
            await _attendanceService.CreateAttendanceAsync(attendanceDto);
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
