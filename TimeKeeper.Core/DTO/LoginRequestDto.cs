namespace TimeKeeper.Core.DTO
{
    public class LoginRequest
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }

    public class LoginResponse
    {
        public string? Token { get; set; }
        public Guid UserId { get; set; }
        public int EmployeeId { get; set; }
        public List<string>? Permissions { get; set; }
        public string? UserRole { get; set; }
    }
}