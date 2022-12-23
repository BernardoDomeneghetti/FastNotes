using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastNotes.Domain.Migrations
{
    public partial class fileColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileBase64",
                table: "Notes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContentBase64",
                table: "NoteFiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileBase64",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "ContentBase64",
                table: "NoteFiles");
        }
    }
}
