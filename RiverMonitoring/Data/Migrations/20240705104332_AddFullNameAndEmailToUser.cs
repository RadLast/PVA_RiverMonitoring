using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RiverMonitoring.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddFullNameAndEmailToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NickName",
                table: "AspNetUsers",
                newName: "FullName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "AspNetUsers",
                newName: "NickName");
        }
    }
}
