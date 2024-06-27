using System.Globalization;

namespace TimeKeeper.Core.DTO
{
    public class AttendanceEntryDto
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? TimeIn { get; set; }
        public DateTime? TimeOut { get; set; }

        public string? TimeInString { get; set; }
        public string? TimeOutString { get; set; }

        public string? FormattedTimeIn => TimeIn?.ToString("h:mm tt");
        public string? FormattedTimeOut => TimeOut?.ToString("h:mm tt");
        public string? FormattedEntryDate
        {
            get
            {
                return Date.HasValue ? Date.Value.ToString("MMMM dd, yyyy", CultureInfo.InvariantCulture) : string.Empty;
            }
        }
    }
}
