using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AreaBox_V0._1.Migrations
{
    /// <inheritdoc />
    public partial class changeKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionPostsReports",
                table: "QuestionPostsReports");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionPostsReports",
                table: "QuestionPostsReports",
                columns: new[] { "QPostID", "UserId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionPostsReports",
                table: "QuestionPostsReports");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionPostsReports",
                table: "QuestionPostsReports",
                columns: new[] { "QPostID", "ReportTypeID" });
        }
    }
}
