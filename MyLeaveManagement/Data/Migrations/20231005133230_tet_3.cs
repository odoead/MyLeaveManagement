using Microsoft.EntityFrameworkCore.Migrations;
#nullable disable
namespace MyLeaveManagement.Data.Migrations
{
    public partial class tet_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveHistory_AspNetUsers_ApprovedById",
                table: "LeaveHistory"
            );
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveHistory_AspNetUsers_RequestingEmpoyeeId",
                table: "LeaveHistory"
            );
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveHistory_leaveTypes_LeaveTypeId",
                table: "LeaveHistory"
            );
            migrationBuilder.DropPrimaryKey(name: "PK_LeaveHistory", table: "LeaveHistory");
            migrationBuilder.RenameTable(name: "LeaveHistory", newName: "leaveHistories");
            migrationBuilder.RenameIndex(
                name: "IX_LeaveHistory_RequestingEmpoyeeId",
                table: "leaveHistories",
                newName: "IX_leaveHistories_RequestingEmpoyeeId"
            );
            migrationBuilder.RenameIndex(
                name: "IX_LeaveHistory_LeaveTypeId",
                table: "leaveHistories",
                newName: "IX_leaveHistories_LeaveTypeId"
            );
            migrationBuilder.RenameIndex(
                name: "IX_LeaveHistory_ApprovedById",
                table: "leaveHistories",
                newName: "IX_leaveHistories_ApprovedById"
            );
            migrationBuilder.AddPrimaryKey(
                name: "PK_leaveHistories",
                table: "leaveHistories",
                column: "Id"
            );
            migrationBuilder.AddForeignKey(
                name: "FK_leaveHistories_AspNetUsers_ApprovedById",
                table: "leaveHistories",
                column: "ApprovedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id"
            );
            migrationBuilder.AddForeignKey(
                name: "FK_leaveHistories_AspNetUsers_RequestingEmpoyeeId",
                table: "leaveHistories",
                column: "RequestingEmpoyeeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id"
            );
            migrationBuilder.AddForeignKey(
                name: "FK_leaveHistories_leaveTypes_LeaveTypeId",
                table: "leaveHistories",
                column: "LeaveTypeId",
                principalTable: "leaveTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_leaveHistories_AspNetUsers_ApprovedById",
                table: "leaveHistories"
            );
            migrationBuilder.DropForeignKey(
                name: "FK_leaveHistories_AspNetUsers_RequestingEmpoyeeId",
                table: "leaveHistories"
            );
            migrationBuilder.DropForeignKey(
                name: "FK_leaveHistories_leaveTypes_LeaveTypeId",
                table: "leaveHistories"
            );
            migrationBuilder.DropPrimaryKey(name: "PK_leaveHistories", table: "leaveHistories");
            migrationBuilder.RenameTable(name: "leaveHistories", newName: "LeaveHistory");
            migrationBuilder.RenameIndex(
                name: "IX_leaveHistories_RequestingEmpoyeeId",
                table: "LeaveHistory",
                newName: "IX_LeaveHistory_RequestingEmpoyeeId"
            );
            migrationBuilder.RenameIndex(
                name: "IX_leaveHistories_LeaveTypeId",
                table: "LeaveHistory",
                newName: "IX_LeaveHistory_LeaveTypeId"
            );
            migrationBuilder.RenameIndex(
                name: "IX_leaveHistories_ApprovedById",
                table: "LeaveHistory",
                newName: "IX_LeaveHistory_ApprovedById"
            );
            migrationBuilder.AddPrimaryKey(
                name: "PK_LeaveHistory",
                table: "LeaveHistory",
                column: "Id"
            );
            migrationBuilder.AddForeignKey(
                name: "FK_LeaveHistory_AspNetUsers_ApprovedById",
                table: "LeaveHistory",
                column: "ApprovedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id"
            );
            migrationBuilder.AddForeignKey(
                name: "FK_LeaveHistory_AspNetUsers_RequestingEmpoyeeId",
                table: "LeaveHistory",
                column: "RequestingEmpoyeeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id"
            );
            migrationBuilder.AddForeignKey(
                name: "FK_LeaveHistory_leaveTypes_LeaveTypeId",
                table: "LeaveHistory",
                column: "LeaveTypeId",
                principalTable: "leaveTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );
        }
    }
}
