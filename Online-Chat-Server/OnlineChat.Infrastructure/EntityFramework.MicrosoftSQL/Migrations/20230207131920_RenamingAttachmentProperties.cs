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

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "719c949a-1d9e-4fbe-9ac2-3f7c36c7d7a5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "a9454543-ff78-4324-b13b-73a73caaed33");
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

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "114845f6-832f-4431-8538-5af249f0a161");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "5b98b53d-2571-4a74-a6e3-a0a53764dd0b");
        }
    }
}
