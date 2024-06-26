namespace TimeKeeper.Core.DTO
{
    public class AttendanceEntryDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan? TimeIn { get; set; }
        public TimeSpan? TimeOut { get; set; }
    }
}
