using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeKeeper.Infrastructure.Migrations
{
    public partial class _008_Employee_Seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserDetails",
                keyColumn: "UserId",
                keyValue: new Guid("397d7756-2968-4749-9b7f-6f5a111a3ee2"));

            migrationBuilder.InsertData(
                table: "UserDetails",
                columns: new[] { "UserId", "Email", "FirstName", "LastName", "PasswordHash", "PhoneNumber", "RoleId", "Salt" },
                values: new object[] { new Guid("6cd469ae-7654-4e76-a008-c5c262cdd123"), "admin@user.com", "Admin", "User", "O9Am/GJ39Oje93VgSdi9AinvfD4o7aN+YvSmNSP5I+A=", "1234567890", 1, "N2rzdQo97l1o4EiArJapPA==" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "UserId" },
                values: new object[] { 1, new Guid("6cd469ae-7654-4e76-a008-c5c262cdd123") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserDetails",
                keyColumn: "UserId",
                keyValue: new Guid("6cd469ae-7654-4e76-a008-c5c262cdd123"));

            migrationBuilder.InsertData(
                table: "UserDetails",
                columns: new[] { "UserId", "Email", "FirstName", "LastName", "PasswordHash", "PhoneNumber", "RoleId", "Salt" },
                values: new object[] { new Guid("397d7756-2968-4749-9b7f-6f5a111a3ee2"), "admin@user.com", "Admin", "User", "wGcLFFXSv/rJD50G0nNXeBeH20/bUlQUBovRz5yuHoY=", "1234567890", 1, "61J9h/J4pGDDX5mfHo2Llw==" });
        }
    }
}
