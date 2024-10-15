using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace to_do.Migrations
{
    /// <inheritdoc />
    public partial class InitialAddToDoList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_ToDoLists_ToDoListId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_ToDoListId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ToDoListId",
                table: "Items");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ToDoListId",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_ToDoListId",
                table: "Items",
                column: "ToDoListId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_ToDoLists_ToDoListId",
                table: "Items",
                column: "ToDoListId",
                principalTable: "ToDoLists",
                principalColumn: "Id");
        }
    }
}
