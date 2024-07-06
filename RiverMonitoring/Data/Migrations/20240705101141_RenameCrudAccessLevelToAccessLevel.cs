using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RiverMonitoring.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameCrudAccessLevelToAccessLevel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CrudAccessLevel",
                table: "AspNetUsers",
                newName: "AccessLevel");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AccessLevel",
                table: "AspNetUsers",
                newName: "CrudAccessLevel");
        }
    }
}
