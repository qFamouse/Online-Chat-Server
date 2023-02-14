using MassTransit.Middleware;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityFramework.MicrosoftSQL.Migrations
{
    public partial class AddingStatisticFunction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
CREATE FUNCTION GetStatisticByUserId
	(@UserId INT)
RETURNS @statistic TABLE
(
	TotalMessages	INT					NOT NULL,
	TotalSent		INT					NOT NULL,
	TotalReceived	INT					NOT NULL

)
AS 
BEGIN
	DECLARE @TotalSent as INT = (SELECT COUNT(*) FROM DirectMessages dm WHERE dm.SenderId = @UserId);
	DECLARE @TotalReceived INT = (SELECT COUNT(*) FROM DirectMessages dm WHERE dm.ReceiverId = @UserId);
	DECLARE @TotalMessages as INT = (@TotalSent + @TotalReceived);

	INSERT @statistic
		SELECT @TotalMessages, @TotalSent, @TotalReceived
RETURN
END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP FUNCTION GetStatisticByUserId;");
        }
    }
}
