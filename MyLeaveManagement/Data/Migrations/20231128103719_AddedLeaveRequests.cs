using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyLeaveManagement.Data.Migrations
{
    public partial class AddedLeaveRequests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_leaveAllocations_AspNetUsers_EmployeeId",
                table: "leaveAllocations");

            migrationBuilder.DropForeignKey(
                name: "FK_leaveAllocations_LeaveTypes_LeaveTypeId",
                table: "leaveAllocations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_leaveAllocations",
                table: "leaveAllocations");

            migrationBuilder.RenameTable(
                name: "leaveAllocations",
                newName: "LeaveAllocations");

            migrationBuilder.RenameIndex(
                name: "IX_leaveAllocations_LeaveTypeId",
                table: "LeaveAllocations",
                newName: "IX_LeaveAllocations_LeaveTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_leaveAllocations_EmployeeId",
                table: "LeaveAllocations",
                newName: "IX_LeaveAllocations_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LeaveAllocations",
                table: "LeaveAllocations",
                column: "id");

            migrationBuilder.CreateTable(
                name: "EmployeeViewModel",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaxId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateJoined = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeViewModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LeaveTypeViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DefaultDays = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveTypeViewModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LeaveRequestViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestingEmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    RequestingEmpoyeeId = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_LeaveRequestViewModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaveRequestViewModel_EmployeeViewModel_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "EmployeeViewModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeaveRequestViewModel_EmployeeViewModel_RequestingEmployeeId",
                        column: x => x.RequestingEmployeeId,
                        principalTable: "EmployeeViewModel",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LeaveRequestViewModel_LeaveTypeViewModel_LeaveTypeId",
                        column: x => x.LeaveTypeId,
                        principalTable: "LeaveTypeViewModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequestViewModel_ApprovedById",
                table: "LeaveRequestViewModel",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequestViewModel_LeaveTypeId",
                table: "LeaveRequestViewModel",
                column: "LeaveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequestViewModel_RequestingEmployeeId",
                table: "LeaveRequestViewModel",
                column: "RequestingEmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveAllocations_AspNetUsers_EmployeeId",
                table: "LeaveAllocations",
                column: "EmployeeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveAllocations_LeaveTypes_LeaveTypeId",
                table: "LeaveAllocations",
                column: "LeaveTypeId",
                principalTable: "LeaveTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveAllocations_AspNetUsers_EmployeeId",
                table: "LeaveAllocations");

            migrationBuilder.DropForeignKey(
                name: "FK_LeaveAllocations_LeaveTypes_LeaveTypeId",
                table: "LeaveAllocations");

            migrationBuilder.DropTable(
                name: "LeaveRequestViewModel");

            migrationBuilder.DropTable(
                name: "EmployeeViewModel");

            migrationBuilder.DropTable(
                name: "LeaveTypeViewModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LeaveAllocations",
                table: "LeaveAllocations");

            migrationBuilder.RenameTable(
                name: "LeaveAllocations",
                newName: "leaveAllocations");

            migrationBuilder.RenameIndex(
                name: "IX_LeaveAllocations_LeaveTypeId",
                table: "leaveAllocations",
                newName: "IX_leaveAllocations_LeaveTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_LeaveAllocations_EmployeeId",
                table: "leaveAllocations",
                newName: "IX_leaveAllocations_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_leaveAllocations",
                table: "leaveAllocations",
                column: "id");

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
    }
}
