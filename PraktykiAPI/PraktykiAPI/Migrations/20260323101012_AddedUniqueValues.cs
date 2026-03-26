using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PraktykiAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedUniqueValues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropIndex(
            //    name: "IX_Users_Login",
            //    table: "Users");

            //migrationBuilder.DropIndex(
            //    name: "IX_Employees_Email",
            //    table: "Employees");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_Login",
                table: "Users",
                column: "Login",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Email",
                table: "Employees",
                column: "Email",
                unique: true);
        }
    }
}
