namespace TimeKeeper.Core.DTO
{
    public class AttendanceEntryDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime? TimeIn { get; set; }
        public DateTime? TimeOut { get; set; }
    }
}
