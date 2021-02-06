using Microsoft.EntityFrameworkCore.Migrations;

namespace ChatApp.Migrations
{
    public partial class addedmessagereceiverconnectionIdpropertyinmessages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "connectionid",
                table: "Messages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "receiver",
                table: "Messages",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "connectionid",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "receiver",
                table: "Messages");
        }
    }
}
