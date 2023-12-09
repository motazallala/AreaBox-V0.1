using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AreaBox_V0._1.Migrations
{
    /// <inheritdoc />
    public partial class deletehabed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersMediaPostComments");

            migrationBuilder.DropTable(
                name: "UsersQusetionPostComments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsersMediaPostComments",
                columns: table => new
                {
                    MPCommentID = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_UsersMediaPostComments_AspNetUsers",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UsersMediaPostComments_MediaPostComments",
                        column: x => x.MPCommentID,
                        principalTable: "MediaPostComments",
                        principalColumn: "MPCommentID");
                });

            migrationBuilder.CreateTable(
                name: "UsersQusetionPostComments",
                columns: table => new
                {
                    QPCommentID = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_UsersQusetionPostComments_AspNetUsers",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UsersQusetionPostComments_QuestionPostComments",
                        column: x => x.QPCommentID,
                        principalTable: "QuestionPostComments",
                        principalColumn: "QPCommentID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsersMediaPostComments_MPCommentID",
                table: "UsersMediaPostComments",
                column: "MPCommentID");

            migrationBuilder.CreateIndex(
                name: "IX_UsersMediaPostComments_UserID",
                table: "UsersMediaPostComments",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_UsersQusetionPostComments_QPCommentID",
                table: "UsersQusetionPostComments",
                column: "QPCommentID");

            migrationBuilder.CreateIndex(
                name: "IX_UsersQusetionPostComments_UserID",
                table: "UsersQusetionPostComments",
                column: "UserID");
        }
    }
}
