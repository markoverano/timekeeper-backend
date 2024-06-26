namespace TimeKeeper.Core.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public UserDetail? UserDetails { get; set; }
    }
}