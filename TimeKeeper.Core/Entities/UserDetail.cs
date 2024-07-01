namespace TimeKeeper.Core.Entities
{
    public class UserDetail
    {
        public Guid UserId { get; set; }
        public string? FirstName { get; set; }
        public string ? LastName { get; set; }
        public string ? Email { get; set; }
        public string ? PhoneNumber { get; set; }
        public int RoleId { get; set; }
        public UserRole? Role { get; set; }
        public string ? PasswordHash { get; set; }
        public ICollection<RolePermission>? RolePermissions { get; set; }
        public string? Salt { get; set; }
    }
}