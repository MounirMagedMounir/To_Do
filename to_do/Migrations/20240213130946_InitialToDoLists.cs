using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace to_do.Migrations
{
    /// <inheritdoc />
    public partial class InitialToDoLists : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Discriminator",
                table: "Items",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(8)",
                oldMaxLength: 8);

            migrationBuilder.AddColumn<int>(
                name: "ToDoListsId",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_ToDoListsId",
                table: "Items",
                column: "ToDoListsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Items_ToDoListsId",
                table: "Items",
                column: "ToDoListsId",
                principalTable: "Items",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Items_ToDoListsId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_ToDoListsId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ToDoListsId",
                table: "Items");

            migrationBuilder.AlterColumn<string>(
                name: "Discriminator",
                table: "Items",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(13)",
                oldMaxLength: 13);
        }
    }
}
