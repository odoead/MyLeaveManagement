using Microsoft.EntityFrameworkCore.Migrations;
#nullable disable
namespace MyLeaveManagement.Data.Migrations
{
    public partial class fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(name: "id", table: "LeaveAllocations", newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(name: "Id", table: "LeaveAllocations", newName: "id");
        }
    }
}
