using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PraktykiAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixedForeignKeyInUserClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Employees_Employee_ID",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_Employee_ID",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Employee_ID",
                table: "Users");

            migrationBuilder.CreateIndex(
                name: "IX_Users_EmployeeID",
                table: "Users",
                column: "EmployeeID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Employees_EmployeeID",
                table: "Users",
                column: "EmployeeID",
                principalTable: "Employees",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Employees_EmployeeID",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_EmployeeID",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "Employee_ID",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Employee_ID",
                table: "Users",
                column: "Employee_ID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Employees_Employee_ID",
                table: "Users",
                column: "Employee_ID",
                principalTable: "Employees",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
