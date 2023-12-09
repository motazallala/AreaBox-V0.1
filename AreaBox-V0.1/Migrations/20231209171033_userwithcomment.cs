using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AreaBox_V0._1.Migrations
{
    /// <inheritdoc />
    public partial class userwithcomment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "QuestionPostComments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "MediaPostComments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionPostComments_UserId",
                table: "QuestionPostComments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MediaPostComments_UserId",
                table: "MediaPostComments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MediaPostComments_Users",
                table: "MediaPostComments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionPostComments_Users",
                table: "QuestionPostComments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MediaPostComments_Users",
                table: "MediaPostComments");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionPostComments_Users",
                table: "QuestionPostComments");

            migrationBuilder.DropIndex(
                name: "IX_QuestionPostComments_UserId",
                table: "QuestionPostComments");

            migrationBuilder.DropIndex(
                name: "IX_MediaPostComments_UserId",
                table: "MediaPostComments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "QuestionPostComments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "MediaPostComments");
        }
    }
}
