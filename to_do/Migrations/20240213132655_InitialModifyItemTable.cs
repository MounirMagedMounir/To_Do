using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace to_do.Migrations
{
    /// <inheritdoc />
    public partial class InitialModifyItemTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Duo",
                table: "Items",
                newName: "Date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Items",
                newName: "Duo");
        }
    }
}
