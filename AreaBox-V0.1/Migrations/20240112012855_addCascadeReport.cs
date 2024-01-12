using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AreaBox_V0._1.Migrations
{
    /// <inheritdoc />
    public partial class addCascadeReport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MediaPostsReports_ReportTypes",
                table: "MediaPostsReports");

            migrationBuilder.AddForeignKey(
                name: "FK_MediaPostsReports_ReportTypes",
                table: "MediaPostsReports",
                column: "PostReportID",
                principalTable: "PostReports",
                principalColumn: "PostReportId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MediaPostsReports_ReportTypes",
                table: "MediaPostsReports");

            migrationBuilder.AddForeignKey(
                name: "FK_MediaPostsReports_ReportTypes",
                table: "MediaPostsReports",
                column: "PostReportID",
                principalTable: "PostReports",
                principalColumn: "PostReportId");
        }
    }
}
