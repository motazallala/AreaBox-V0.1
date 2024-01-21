using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AreaBox_V0._1.Migrations
{
    /// <inheritdoc />
    public partial class addCascadeToCategoryRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MediaPosts_Categories",
                table: "MediaPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionPosts_Categories",
                table: "QuestionPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCategories_Category",
                table: "UserCategories");

            migrationBuilder.AddForeignKey(
                name: "FK_MediaPosts_Categories",
                table: "MediaPosts",
                column: "MPCategoryID",
                principalTable: "Categories",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionPosts_Categories",
                table: "QuestionPosts",
                column: "QPCategoryID",
                principalTable: "Categories",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCategories_Category",
                table: "UserCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MediaPosts_Categories",
                table: "MediaPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionPosts_Categories",
                table: "QuestionPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCategories_Category",
                table: "UserCategories");

            migrationBuilder.AddForeignKey(
                name: "FK_MediaPosts_Categories",
                table: "MediaPosts",
                column: "MPCategoryID",
                principalTable: "Categories",
                principalColumn: "CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionPosts_Categories",
                table: "QuestionPosts",
                column: "QPCategoryID",
                principalTable: "Categories",
                principalColumn: "CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCategories_Category",
                table: "UserCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryID");
        }
    }
}
