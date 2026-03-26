using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PraktykiAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddWorkSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkSettings",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MinWorkdayLengthInMinutes = table.Column<int>(type: "int", nullable: true),
                    AutoEndWorkdayLengthInMinutes = table.Column<int>(type: "int", nullable: true),
                    MinBreakBetweenWorkdaysInMinutes = table.Column<int>(type: "int", nullable: true),
                    MinWorkdayLengthForBreakInMinutes = table.Column<int>(type: "int", nullable: true),
                    MinBreakLengthInMinutes = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkSettings", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkSettings");
        }
    }
}
