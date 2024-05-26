using Microsoft.EntityFrameworkCore.Migrations;
#nullable disable
namespace MyLeaveManagement.Data.Migrations
{
    public partial class testM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TestDays",
                table: "LeaveTypes",
                type: "int",
                nullable: true
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "TestDays", table: "LeaveTypes");
        }
    }
}
