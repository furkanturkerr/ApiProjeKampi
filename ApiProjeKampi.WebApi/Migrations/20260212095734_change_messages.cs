using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiProjeKampi.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class change_messages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MessageDate",
                table: "Message",
                newName: "SendDate");

            migrationBuilder.RenameColumn(
                name: "MessageBody",
                table: "Message",
                newName: "MessageDetails");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SendDate",
                table: "Message",
                newName: "MessageDate");

            migrationBuilder.RenameColumn(
                name: "MessageDetails",
                table: "Message",
                newName: "MessageBody");
        }
    }
}
