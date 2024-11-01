using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ABCDoubleE.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserPasswordFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "password",
                table: "Users",
                newName: "passwordSalt");

            migrationBuilder.AddColumn<string>(
                name: "passwordHash",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "passwordHash",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "passwordSalt",
                table: "Users",
                newName: "password");
        }
    }
}
