using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AreaBox_V0._1.Migrations
{
    /// <inheritdoc />
    public partial class addCascadeMediaPostReport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MediaPostsReports_MediaPosts",
                table: "MediaPostsReports");

            migrationBuilder.AddForeignKey(
                name: "FK_MediaPostsReports_MediaPosts",
                table: "MediaPostsReports",
                column: "MPostID",
                principalTable: "MediaPosts",
                principalColumn: "MPostID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MediaPostsReports_MediaPosts",
                table: "MediaPostsReports");

            migrationBuilder.AddForeignKey(
                name: "FK_MediaPostsReports_MediaPosts",
                table: "MediaPostsReports",
                column: "MPostID",
                principalTable: "MediaPosts",
                principalColumn: "MPostID");
        }
    }
}
