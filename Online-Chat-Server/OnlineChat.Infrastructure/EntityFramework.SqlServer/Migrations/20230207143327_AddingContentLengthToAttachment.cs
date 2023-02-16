using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityFramework.SqlServer.Migrations
{
    public partial class AddingContentLengthToAttachment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ContentLength",
                table: "Attachments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentLength",
                table: "Attachments");
        }
    }
}
