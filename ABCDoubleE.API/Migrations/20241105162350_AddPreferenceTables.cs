using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ABCDoubleE.API.Migrations
{
    /// <inheritdoc />
    public partial class AddPreferenceTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PreferenceAuthor_Authors_authorId",
                table: "PreferenceAuthor");

            migrationBuilder.DropForeignKey(
                name: "FK_PreferenceAuthor_Preferences_preferenceId",
                table: "PreferenceAuthor");

            migrationBuilder.DropForeignKey(
                name: "FK_PreferenceBook_Books_bookId",
                table: "PreferenceBook");

            migrationBuilder.DropForeignKey(
                name: "FK_PreferenceBook_Preferences_preferenceId",
                table: "PreferenceBook");

            migrationBuilder.DropForeignKey(
                name: "FK_PreferenceGenre_Genres_genreId",
                table: "PreferenceGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_PreferenceGenre_Preferences_preferenceId",
                table: "PreferenceGenre");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PreferenceGenre",
                table: "PreferenceGenre");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PreferenceBook",
                table: "PreferenceBook");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PreferenceAuthor",
                table: "PreferenceAuthor");

            migrationBuilder.RenameTable(
                name: "PreferenceGenre",
                newName: "PreferenceGenres");

            migrationBuilder.RenameTable(
                name: "PreferenceBook",
                newName: "PreferenceBooks");

            migrationBuilder.RenameTable(
                name: "PreferenceAuthor",
                newName: "PreferenceAuthors");

            migrationBuilder.RenameIndex(
                name: "IX_PreferenceGenre_genreId",
                table: "PreferenceGenres",
                newName: "IX_PreferenceGenres_genreId");

            migrationBuilder.RenameIndex(
                name: "IX_PreferenceBook_bookId",
                table: "PreferenceBooks",
                newName: "IX_PreferenceBooks_bookId");

            migrationBuilder.RenameIndex(
                name: "IX_PreferenceAuthor_authorId",
                table: "PreferenceAuthors",
                newName: "IX_PreferenceAuthors_authorId");

            migrationBuilder.AddColumn<string>(
                name: "title",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PreferenceGenres",
                table: "PreferenceGenres",
                columns: new[] { "preferenceId", "genreId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PreferenceBooks",
                table: "PreferenceBooks",
                columns: new[] { "preferenceId", "bookId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PreferenceAuthors",
                table: "PreferenceAuthors",
                columns: new[] { "preferenceId", "authorId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PreferenceAuthors_Authors_authorId",
                table: "PreferenceAuthors",
                column: "authorId",
                principalTable: "Authors",
                principalColumn: "authorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PreferenceAuthors_Preferences_preferenceId",
                table: "PreferenceAuthors",
                column: "preferenceId",
                principalTable: "Preferences",
                principalColumn: "preferenceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PreferenceBooks_Books_bookId",
                table: "PreferenceBooks",
                column: "bookId",
                principalTable: "Books",
                principalColumn: "bookId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PreferenceBooks_Preferences_preferenceId",
                table: "PreferenceBooks",
                column: "preferenceId",
                principalTable: "Preferences",
                principalColumn: "preferenceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PreferenceGenres_Genres_genreId",
                table: "PreferenceGenres",
                column: "genreId",
                principalTable: "Genres",
                principalColumn: "genreId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PreferenceGenres_Preferences_preferenceId",
                table: "PreferenceGenres",
                column: "preferenceId",
                principalTable: "Preferences",
                principalColumn: "preferenceId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PreferenceAuthors_Authors_authorId",
                table: "PreferenceAuthors");

            migrationBuilder.DropForeignKey(
                name: "FK_PreferenceAuthors_Preferences_preferenceId",
                table: "PreferenceAuthors");

            migrationBuilder.DropForeignKey(
                name: "FK_PreferenceBooks_Books_bookId",
                table: "PreferenceBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_PreferenceBooks_Preferences_preferenceId",
                table: "PreferenceBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_PreferenceGenres_Genres_genreId",
                table: "PreferenceGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_PreferenceGenres_Preferences_preferenceId",
                table: "PreferenceGenres");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PreferenceGenres",
                table: "PreferenceGenres");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PreferenceBooks",
                table: "PreferenceBooks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PreferenceAuthors",
                table: "PreferenceAuthors");

            migrationBuilder.DropColumn(
                name: "title",
                table: "Books");

            migrationBuilder.RenameTable(
                name: "PreferenceGenres",
                newName: "PreferenceGenre");

            migrationBuilder.RenameTable(
                name: "PreferenceBooks",
                newName: "PreferenceBook");

            migrationBuilder.RenameTable(
                name: "PreferenceAuthors",
                newName: "PreferenceAuthor");

            migrationBuilder.RenameIndex(
                name: "IX_PreferenceGenres_genreId",
                table: "PreferenceGenre",
                newName: "IX_PreferenceGenre_genreId");

            migrationBuilder.RenameIndex(
                name: "IX_PreferenceBooks_bookId",
                table: "PreferenceBook",
                newName: "IX_PreferenceBook_bookId");

            migrationBuilder.RenameIndex(
                name: "IX_PreferenceAuthors_authorId",
                table: "PreferenceAuthor",
                newName: "IX_PreferenceAuthor_authorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PreferenceGenre",
                table: "PreferenceGenre",
                columns: new[] { "preferenceId", "genreId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PreferenceBook",
                table: "PreferenceBook",
                columns: new[] { "preferenceId", "bookId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PreferenceAuthor",
                table: "PreferenceAuthor",
                columns: new[] { "preferenceId", "authorId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PreferenceAuthor_Authors_authorId",
                table: "PreferenceAuthor",
                column: "authorId",
                principalTable: "Authors",
                principalColumn: "authorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PreferenceAuthor_Preferences_preferenceId",
                table: "PreferenceAuthor",
                column: "preferenceId",
                principalTable: "Preferences",
                principalColumn: "preferenceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PreferenceBook_Books_bookId",
                table: "PreferenceBook",
                column: "bookId",
                principalTable: "Books",
                principalColumn: "bookId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PreferenceBook_Preferences_preferenceId",
                table: "PreferenceBook",
                column: "preferenceId",
                principalTable: "Preferences",
                principalColumn: "preferenceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PreferenceGenre_Genres_genreId",
                table: "PreferenceGenre",
                column: "genreId",
                principalTable: "Genres",
                principalColumn: "genreId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PreferenceGenre_Preferences_preferenceId",
                table: "PreferenceGenre",
                column: "preferenceId",
                principalTable: "Preferences",
                principalColumn: "preferenceId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
