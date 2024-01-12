using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AreaBox_V0._1.Migrations
{
    /// <inheritdoc />
    public partial class addCascadeToLike : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MediaPostLikes_MediaPosts",
                table: "MediaPostLikes");

            migrationBuilder.AddForeignKey(
                name: "FK_MediaPostLikes_MediaPosts",
                table: "MediaPostLikes",
                column: "MPostID",
                principalTable: "MediaPosts",
                principalColumn: "MPostID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MediaPostLikes_MediaPosts",
                table: "MediaPostLikes");

            migrationBuilder.AddForeignKey(
                name: "FK_MediaPostLikes_MediaPosts",
                table: "MediaPostLikes",
                column: "MPostID",
                principalTable: "MediaPosts",
                principalColumn: "MPostID");
        }
    }
}
