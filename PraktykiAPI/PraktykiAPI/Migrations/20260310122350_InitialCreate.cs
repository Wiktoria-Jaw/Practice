using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PraktykiAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Break_Timetable_WorkDay_Id",
                table: "Break_Timetable",
                column: "WorkDay_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Days_Off_Employee_ID",
                table: "Days_Off",
                column: "Employee_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Employee_ID",
                table: "Users",
                column: "Employee_ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Work_Timetable_Employee_Id",
                table: "Work_Timetable",
                column: "Employee_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
