using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ABCDoubleE.API.Migrations
{
    /// <inheritdoc />
    public partial class TestingnewDBContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Preferences_PreferencespreferenceId",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "PreferencespreferenceId",
                table: "Books",
                newName: "preferenceId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_PreferencespreferenceId",
                table: "Books",
                newName: "IX_Books_preferenceId");

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "Bookshelves",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Preferences_preferenceId",
                table: "Books",
                column: "preferenceId",
                principalTable: "Preferences",
                principalColumn: "preferenceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Preferences_preferenceId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "name",
                table: "Bookshelves");

            migrationBuilder.RenameColumn(
                name: "preferenceId",
                table: "Books",
                newName: "PreferencespreferenceId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_preferenceId",
                table: "Books",
                newName: "IX_Books_PreferencespreferenceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Preferences_PreferencespreferenceId",
                table: "Books",
                column: "PreferencespreferenceId",
                principalTable: "Preferences",
                principalColumn: "preferenceId");
        }
    }
}
