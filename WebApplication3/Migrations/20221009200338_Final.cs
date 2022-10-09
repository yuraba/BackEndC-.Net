using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication3.Migrations
{
    /// <inheritdoc />
    public partial class Final : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Articles",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Articles",
                newName: "image");

            migrationBuilder.RenameColumn(
                name: "Body",
                table: "Articles",
                newName: "body");

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "title",
                table: "Articles",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "image",
                table: "Articles",
                newName: "Image");

            migrationBuilder.RenameColumn(
                name: "body",
                table: "Articles",
                newName: "Body");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Articles",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
