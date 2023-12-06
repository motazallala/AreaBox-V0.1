using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AreaBox_V0._1.Migrations
{
    /// <inheritdoc />
    public partial class InitNewMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DOB = table.Column<DateTime>(type: "Date", nullable: false),
                    ProfilePicture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bio = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    State = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryID);
                });

            migrationBuilder.CreateTable(
                name: "PostTypes",
                columns: table => new
                {
                    PostTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostTypes", x => x.PostTypeId);
                });

            migrationBuilder.CreateTable(
                name: "ReportTypes",
                columns: table => new
                {
                    ReportTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportTypes", x => x.ReportTypeId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TechnicalReports",
                columns: table => new
                {
                    TechnicalReportID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicalReports", x => x.TechnicalReportID);
                    table.ForeignKey(
                        name: "FK_TechnicalReports_AspNetUsers",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserCategories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCategories", x => new { x.UserID, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_UserCategories_AspNetUsers",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserCategories_Category",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryID");
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    CitryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CountryID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.CitryID);
                    table.ForeignKey(
                        name: "FK_Cities_Countries",
                        column: x => x.CountryID,
                        principalTable: "Countries",
                        principalColumn: "CountryID");
                });

            migrationBuilder.CreateTable(
                name: "PostReports",
                columns: table => new
                {
                    PostReportId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostTypeId = table.Column<int>(type: "int", nullable: false),
                    ReportTypeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostReports", x => x.PostReportId);
                    table.ForeignKey(
                        name: "FK_PostReports_PostTypes_PostTypeId",
                        column: x => x.PostTypeId,
                        principalTable: "PostTypes",
                        principalColumn: "PostTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostReports_ReportTypes_ReportTypeID",
                        column: x => x.ReportTypeID,
                        principalTable: "ReportTypes",
                        principalColumn: "ReportTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MediaPosts",
                columns: table => new
                {
                    MPostID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MPCategoryID = table.Column<int>(type: "int", nullable: false),
                    MPCityID = table.Column<int>(type: "int", nullable: false),
                    MPDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    MPUserID = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    MPImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MPShortDescription = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    MPLongDescription = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    MPState = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaPosts", x => x.MPostID);
                    table.ForeignKey(
                        name: "FK_MediaPosts_AspNetUsers",
                        column: x => x.MPUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MediaPosts_Categories",
                        column: x => x.MPCategoryID,
                        principalTable: "Categories",
                        principalColumn: "CategoryID");
                    table.ForeignKey(
                        name: "FK_MediaPosts_Cities",
                        column: x => x.MPCityID,
                        principalTable: "Cities",
                        principalColumn: "CitryID");
                });

            migrationBuilder.CreateTable(
                name: "QuestionPosts",
                columns: table => new
                {
                    QPostID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    QPCategoryID = table.Column<int>(type: "int", nullable: false),
                    QPCityID = table.Column<int>(type: "int", nullable: false),
                    QPDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    QPUserID = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    QPTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    QPDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QPState = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionPosts", x => x.QPostID);
                    table.ForeignKey(
                        name: "FK_QuestionPosts_AspNetUsers",
                        column: x => x.QPUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_QuestionPosts_Categories",
                        column: x => x.QPCategoryID,
                        principalTable: "Categories",
                        principalColumn: "CategoryID");
                    table.ForeignKey(
                        name: "FK_QuestionPosts_Cities",
                        column: x => x.QPCityID,
                        principalTable: "Cities",
                        principalColumn: "CitryID");
                });

            migrationBuilder.CreateTable(
                name: "MediaPostComments",
                columns: table => new
                {
                    MPCommentID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MPostID = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    MPCommnetDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    MPCommentContent = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaPostComments", x => x.MPCommentID);
                    table.ForeignKey(
                        name: "FK_MediaPostComments_MediaPosts",
                        column: x => x.MPostID,
                        principalTable: "MediaPosts",
                        principalColumn: "MPostID");
                });

            migrationBuilder.CreateTable(
                name: "MediaPostLikes",
                columns: table => new
                {
                    MPostID = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaPostLikes", x => new { x.MPostID, x.UserID });
                    table.ForeignKey(
                        name: "FK_MediaPostLikes_AspNetUsers",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MediaPostLikes_MediaPosts",
                        column: x => x.MPostID,
                        principalTable: "MediaPosts",
                        principalColumn: "MPostID");
                });

            migrationBuilder.CreateTable(
                name: "MediaPostsReports",
                columns: table => new
                {
                    ReportTypeID = table.Column<int>(type: "int", nullable: false),
                    MPostID = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_MediaPostsReports_MediaPosts",
                        column: x => x.MPostID,
                        principalTable: "MediaPosts",
                        principalColumn: "MPostID");
                    table.ForeignKey(
                        name: "FK_MediaPostsReports_ReportTypes",
                        column: x => x.ReportTypeID,
                        principalTable: "PostReports",
                        principalColumn: "PostReportId");
                });

            migrationBuilder.CreateTable(
                name: "QuestionPostComments",
                columns: table => new
                {
                    QPCommentID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    QPostID = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    QPCommentDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    QPCommentContent = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionPostComments", x => x.QPCommentID);
                    table.ForeignKey(
                        name: "FK_QuestionPostComments_QuestionPosts",
                        column: x => x.QPostID,
                        principalTable: "QuestionPosts",
                        principalColumn: "QPostID");
                });

            migrationBuilder.CreateTable(
                name: "QuestionPostsReports",
                columns: table => new
                {
                    ReportTypeID = table.Column<int>(type: "int", nullable: false),
                    QPostID = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_QuestionPostsReports_QuestionPosts",
                        column: x => x.QPostID,
                        principalTable: "QuestionPosts",
                        principalColumn: "QPostID");
                    table.ForeignKey(
                        name: "FK_QuestionPostsReports_ReportTypes",
                        column: x => x.ReportTypeID,
                        principalTable: "PostReports",
                        principalColumn: "PostReportId");
                });

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
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CountryID",
                table: "Cities",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_MediaPostComments_MPostID",
                table: "MediaPostComments",
                column: "MPostID");

            migrationBuilder.CreateIndex(
                name: "IX_MediaPostLikes_UserID",
                table: "MediaPostLikes",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_MediaPosts_MPCategoryID",
                table: "MediaPosts",
                column: "MPCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_MediaPosts_MPCityID",
                table: "MediaPosts",
                column: "MPCityID");

            migrationBuilder.CreateIndex(
                name: "IX_MediaPosts_MPUserID",
                table: "MediaPosts",
                column: "MPUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MediaPostsReports_MPostID",
                table: "MediaPostsReports",
                column: "MPostID");

            migrationBuilder.CreateIndex(
                name: "IX_MediaPostsReports_ReportTypeID",
                table: "MediaPostsReports",
                column: "ReportTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_PostReports_PostTypeId",
                table: "PostReports",
                column: "PostTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PostReports_ReportTypeID",
                table: "PostReports",
                column: "ReportTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionPostComments_QPostID",
                table: "QuestionPostComments",
                column: "QPostID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionPosts_QPCategoryID",
                table: "QuestionPosts",
                column: "QPCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionPosts_QPCityID",
                table: "QuestionPosts",
                column: "QPCityID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionPosts_QPUserID",
                table: "QuestionPosts",
                column: "QPUserID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionPostsReports_QPostID",
                table: "QuestionPostsReports",
                column: "QPostID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionPostsReports_ReportTypeID",
                table: "QuestionPostsReports",
                column: "ReportTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_TechnicalReports_UserID",
                table: "TechnicalReports",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserCategories_CategoryId",
                table: "UserCategories",
                column: "CategoryId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "MediaPostLikes");

            migrationBuilder.DropTable(
                name: "MediaPostsReports");

            migrationBuilder.DropTable(
                name: "QuestionPostsReports");

            migrationBuilder.DropTable(
                name: "TechnicalReports");

            migrationBuilder.DropTable(
                name: "UserCategories");

            migrationBuilder.DropTable(
                name: "UsersMediaPostComments");

            migrationBuilder.DropTable(
                name: "UsersQusetionPostComments");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "PostReports");

            migrationBuilder.DropTable(
                name: "MediaPostComments");

            migrationBuilder.DropTable(
                name: "QuestionPostComments");

            migrationBuilder.DropTable(
                name: "PostTypes");

            migrationBuilder.DropTable(
                name: "ReportTypes");

            migrationBuilder.DropTable(
                name: "MediaPosts");

            migrationBuilder.DropTable(
                name: "QuestionPosts");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
