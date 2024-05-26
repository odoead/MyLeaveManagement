using Microsoft.EntityFrameworkCore.Migrations;
#nullable disable
namespace MyLeaveManagement.Data.Migrations
{
    public partial class testM1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "TestDays", table: "LeaveTypes");
            migrationBuilder.AddColumn<string>(
                name: "TestDaysSTR",
                table: "LeaveTypes",
                type: "nvarchar(max)",
                nullable: true
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "TestDaysSTR", table: "LeaveTypes");
            migrationBuilder.AddColumn<int>(
                name: "TestDays",
                table: "LeaveTypes",
                type: "int",
                nullable: true
            );
        }
    }
}
