using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AreaBox_V0._1.Migrations
{
    /// <inheritdoc />
    public partial class addTimeToTechnicalReport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CompleteDateTime",
                table: "TechnicalReports",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CompleteNote",
                table: "TechnicalReports",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReportDateTime",
                table: "TechnicalReports",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ReviewByAdminDateTime",
                table: "TechnicalReports",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ReviewedDateTime",
                table: "TechnicalReports",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompleteDateTime",
                table: "TechnicalReports");

            migrationBuilder.DropColumn(
                name: "CompleteNote",
                table: "TechnicalReports");

            migrationBuilder.DropColumn(
                name: "ReportDateTime",
                table: "TechnicalReports");

            migrationBuilder.DropColumn(
                name: "ReviewByAdminDateTime",
                table: "TechnicalReports");

            migrationBuilder.DropColumn(
                name: "ReviewedDateTime",
                table: "TechnicalReports");
        }
    }
}
