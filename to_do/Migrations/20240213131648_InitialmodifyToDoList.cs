using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace to_do.Migrations
{
    /// <inheritdoc />
    public partial class InitialmodifyToDoList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Items_ToDoListId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Items");

            migrationBuilder.CreateTable(
                name: "ToDoLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDoLists", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Items_ToDoLists_ToDoListId",
                table: "Items",
                column: "ToDoListId",
                principalTable: "ToDoLists",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_ToDoLists_ToDoListId",
                table: "Items");

            migrationBuilder.DropTable(
                name: "ToDoLists");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Items",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Items_ToDoListId",
                table: "Items",
                column: "ToDoListId",
                principalTable: "Items",
                principalColumn: "Id");
        }
    }
}
