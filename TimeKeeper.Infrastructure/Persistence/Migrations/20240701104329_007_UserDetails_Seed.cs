using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeKeeper.Infrastructure.Migrations
{
    public partial class _007_UserDetails_Seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Salt",
                table: "UserDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserDetailUserId",
                table: "RolePermissions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "AttendanceEntries",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.InsertData(
                table: "UserDetails",
                columns: new[] { "UserId", "Email", "FirstName", "LastName", "PasswordHash", "PhoneNumber", "RoleId", "Salt" },
                values: new object[] { new Guid("397d7756-2968-4749-9b7f-6f5a111a3ee2"), "admin@user.com", "Admin", "User", "wGcLFFXSv/rJD50G0nNXeBeH20/bUlQUBovRz5yuHoY=", "1234567890", 1, "61J9h/J4pGDDX5mfHo2Llw==" });

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_UserDetailUserId",
                table: "RolePermissions",
                column: "UserDetailUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_UserDetails_UserDetailUserId",
                table: "RolePermissions",
                column: "UserDetailUserId",
                principalTable: "UserDetails",
                principalColumn: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_UserDetails_UserDetailUserId",
                table: "RolePermissions");

            migrationBuilder.DropIndex(
                name: "IX_RolePermissions_UserDetailUserId",
                table: "RolePermissions");

            migrationBuilder.DeleteData(
                table: "UserDetails",
                keyColumn: "UserId",
                keyValue: new Guid("397d7756-2968-4749-9b7f-6f5a111a3ee2"));

            migrationBuilder.DropColumn(
                name: "Salt",
                table: "UserDetails");

            migrationBuilder.DropColumn(
                name: "UserDetailUserId",
                table: "RolePermissions");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "AttendanceEntries",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
