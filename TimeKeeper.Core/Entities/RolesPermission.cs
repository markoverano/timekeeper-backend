namespace TimeKeeper.Core.Entities
{
    public class RolePermission
    {
        public int RoleId { get; set; }
        public int PermissionId { get; set; }

        public UserRole? Role { get; set; }
        public Permission? Permission { get; set; }
    }
}