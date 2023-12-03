using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyLeaveManagement.Data.Migrations
{
    public partial class LeaveTypeEdit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DefaultDays",
                table: "leaveTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "period",
                table: "LeaveAllocations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DefaultDays",
                table: "DetailsTypeViewModel",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultDays",
                table: "leaveTypes");

            migrationBuilder.DropColumn(
                name: "period",
                table: "LeaveAllocations");

            migrationBuilder.DropColumn(
                name: "DefaultDays",
                table: "DetailsTypeViewModel");
        }
    }
}
