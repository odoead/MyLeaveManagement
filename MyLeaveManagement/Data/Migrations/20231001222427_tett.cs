using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyLeaveManagement.Data.Migrations
{
    public partial class tett : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LeaveHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestingEmpoyeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LeaveTypeId = table.Column<int>(type: "int", nullable: false),
                    DateRequested = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateProvided = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: true),
                    ApprovedById = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaveHistory_AspNetUsers_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LeaveHistory_AspNetUsers_RequestingEmpoyeeId",
                        column: x => x.RequestingEmpoyeeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LeaveHistory_leaveTypes_LeaveTypeId",
                        column: x => x.LeaveTypeId,
                        principalTable: "leaveTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeaveHistory_ApprovedById",
                table: "LeaveHistory",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveHistory_LeaveTypeId",
                table: "LeaveHistory",
                column: "LeaveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveHistory_RequestingEmpoyeeId",
                table: "LeaveHistory",
                column: "RequestingEmpoyeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeaveHistory");
        }
    }
}
