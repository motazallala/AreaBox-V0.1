using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AreaBox_V0._1.Migrations
{
    /// <inheritdoc />
    public partial class addCascadeToReportMANDQ : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MediaPostsReports_ReportType",
                table: "MediaPostsReports");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionPostsReports_QuestionPosts",
                table: "QuestionPostsReports");

            migrationBuilder.AddForeignKey(
                name: "FK_MediaPostsReports_ReportType",
                table: "MediaPostsReports",
                column: "ReportTypeID",
                principalTable: "ReportTypes",
                principalColumn: "ReportTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionPostsReports_QuestionPosts",
                table: "QuestionPostsReports",
                column: "QPostID",
                principalTable: "QuestionPosts",
                principalColumn: "QPostID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MediaPostsReports_ReportType",
                table: "MediaPostsReports");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionPostsReports_QuestionPosts",
                table: "QuestionPostsReports");

            migrationBuilder.AddForeignKey(
                name: "FK_MediaPostsReports_ReportType",
                table: "MediaPostsReports",
                column: "ReportTypeID",
                principalTable: "ReportTypes",
                principalColumn: "ReportTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionPostsReports_QuestionPosts",
                table: "QuestionPostsReports",
                column: "QPostID",
                principalTable: "QuestionPosts",
                principalColumn: "QPostID");
        }
    }
}
