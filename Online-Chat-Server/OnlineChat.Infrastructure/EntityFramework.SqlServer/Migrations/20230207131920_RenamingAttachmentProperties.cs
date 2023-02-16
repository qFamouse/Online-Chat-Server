using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityFramework.MicrosoftSQL.Migrations
{
    public partial class RenamingAttachmentProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TimestampName",
                table: "Attachments",
                newName: "BlobName");

            migrationBuilder.RenameColumn(
                name: "Path",
                table: "Attachments",
                newName: "BlobPath");

            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "Attachments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "Attachments");

            migrationBuilder.RenameColumn(
                name: "BlobName",
                table: "Attachments",
                newName: "TimestampName");

            migrationBuilder.RenameColumn(
                name: "BlobPath",
                table: "Attachments",
                newName: "Path");
        }
    }
}
