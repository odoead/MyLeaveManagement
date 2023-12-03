using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyLeaveManagement.Data.Migrations
{
    public partial class ChangedLeaveHistoryToLeaveRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveAllocations_AspNetUsers_EmployeeId",
                table: "LeaveAllocations");

            migrationBuilder.DropForeignKey(
                name: "FK_LeaveAllocations_leaveTypes_LeaveTypeId",
                table: "LeaveAllocations");

            migrationBuilder.DropTable(
                name: "DetailsTypeViewModel");

            migrationBuilder.DropTable(
                name: "leaveHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_leaveTypes",
                table: "leaveTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LeaveAllocations",
                table: "LeaveAllocations");

            migrationBuilder.RenameTable(
                name: "leaveTypes",
                newName: "LeaveTypes");

            migrationBuilder.RenameTable(
                name: "LeaveAllocations",
                newName: "leaveAllocations");

            migrationBuilder.RenameColumn(
                name: "period",
                table: "leaveAllocations",
                newName: "Period");

            migrationBuilder.RenameIndex(
                name: "IX_LeaveAllocations_LeaveTypeId",
                table: "leaveAllocations",
                newName: "IX_leaveAllocations_LeaveTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_LeaveAllocations_EmployeeId",
                table: "leaveAllocations",
                newName: "IX_leaveAllocations_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LeaveTypes",
                table: "LeaveTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_leaveAllocations",
                table: "leaveAllocations",
                column: "id");

            migrationBuilder.CreateTable(
                name: "LeaveRequests",
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
                    table.PrimaryKey("PK_LeaveRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaveRequests_AspNetUsers_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LeaveRequests_AspNetUsers_RequestingEmpoyeeId",
                        column: x => x.RequestingEmpoyeeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LeaveRequests_LeaveTypes_LeaveTypeId",
                        column: x => x.LeaveTypeId,
                        principalTable: "LeaveTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_ApprovedById",
                table: "LeaveRequests",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_LeaveTypeId",
                table: "LeaveRequests",
                column: "LeaveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_RequestingEmpoyeeId",
                table: "LeaveRequests",
                column: "RequestingEmpoyeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_leaveAllocations_AspNetUsers_EmployeeId",
                table: "leaveAllocations",
                column: "EmployeeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_leaveAllocations_LeaveTypes_LeaveTypeId",
                table: "leaveAllocations",
                column: "LeaveTypeId",
                principalTable: "LeaveTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_leaveAllocations_AspNetUsers_EmployeeId",
                table: "leaveAllocations");

            migrationBuilder.DropForeignKey(
                name: "FK_leaveAllocations_LeaveTypes_LeaveTypeId",
                table: "leaveAllocations");

            migrationBuilder.DropTable(
                name: "LeaveRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LeaveTypes",
                table: "LeaveTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_leaveAllocations",
                table: "leaveAllocations");

            migrationBuilder.RenameTable(
                name: "LeaveTypes",
                newName: "leaveTypes");

            migrationBuilder.RenameTable(
                name: "leaveAllocations",
                newName: "LeaveAllocations");

            migrationBuilder.RenameColumn(
                name: "Period",
                table: "LeaveAllocations",
                newName: "period");

            migrationBuilder.RenameIndex(
                name: "IX_leaveAllocations_LeaveTypeId",
                table: "LeaveAllocations",
                newName: "IX_LeaveAllocations_LeaveTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_leaveAllocations_EmployeeId",
                table: "LeaveAllocations",
                newName: "IX_LeaveAllocations_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_leaveTypes",
                table: "leaveTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LeaveAllocations",
                table: "LeaveAllocations",
                column: "id");

            migrationBuilder.CreateTable(
                name: "DetailsTypeViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DefaultDays = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailsTypeViewModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "leaveHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApprovedById = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LeaveTypeId = table.Column<int>(type: "int", nullable: false),
                    RequestingEmpoyeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateProvided = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateRequested = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_leaveHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_leaveHistories_AspNetUsers_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_leaveHistories_AspNetUsers_RequestingEmpoyeeId",
                        column: x => x.RequestingEmpoyeeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_leaveHistories_leaveTypes_LeaveTypeId",
                        column: x => x.LeaveTypeId,
                        principalTable: "leaveTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_leaveHistories_ApprovedById",
                table: "leaveHistories",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_leaveHistories_LeaveTypeId",
                table: "leaveHistories",
                column: "LeaveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_leaveHistories_RequestingEmpoyeeId",
                table: "leaveHistories",
                column: "RequestingEmpoyeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveAllocations_AspNetUsers_EmployeeId",
                table: "LeaveAllocations",
                column: "EmployeeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveAllocations_leaveTypes_LeaveTypeId",
                table: "LeaveAllocations",
                column: "LeaveTypeId",
                principalTable: "leaveTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
