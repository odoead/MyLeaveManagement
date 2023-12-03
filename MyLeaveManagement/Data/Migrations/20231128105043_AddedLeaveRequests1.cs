using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyLeaveManagement.Data.Migrations
{
    public partial class AddedLeaveRequests1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeaveRequestViewModel");

            migrationBuilder.DropTable(
                name: "EmployeeViewModel");

            migrationBuilder.DropTable(
                name: "LeaveTypeViewModel");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeViewModel",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateJoined = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaxId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DefaultDays = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    ApprovedById = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LeaveTypeId = table.Column<int>(type: "int", nullable: false),
                    RequestingEmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateProvided = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateRequested = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: true),
                    RequestingEmpoyeeId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false)
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
        }
    }
}
