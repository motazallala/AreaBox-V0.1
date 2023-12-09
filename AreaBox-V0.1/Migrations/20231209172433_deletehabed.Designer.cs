﻿// <auto-generated />
using System;
using AreaBox_V0._1.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AreaBox_V0._1.Migrations
{
    [DbContext(typeof(AreaBoxDbContext))]
    [Migration("20231209172433_deletehabed")]
    partial class deletehabed
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AreaBox_V0._1.Data.Model.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Bio")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DOB")
                        .HasColumnType("Date");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("ProfilePicture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("State")
                        .HasColumnType("bit");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("AreaBox_V0._1.Data.Model.Categories", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CategoryID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("AreaBox_V0._1.Data.Model.Cities", b =>
                {
                    b.Property<int>("CitryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CitryID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CitryId"));

                    b.Property<string>("CityName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("CountryId")
                        .HasColumnType("int")
                        .HasColumnName("CountryID");

                    b.HasKey("CitryId");

                    b.HasIndex("CountryId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("AreaBox_V0._1.Data.Model.Countries", b =>
                {
                    b.Property<int>("CountryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CountryID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CountryId"));

                    b.Property<string>("CountryName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("CountryId");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("AreaBox_V0._1.Data.Model.MediaPostComments", b =>
                {
                    b.Property<string>("MpcommentId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("MPCommentID");

                    b.Property<string>("MpcommentContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("MPCommentContent");

                    b.Property<DateTime>("MpcommnetDate")
                        .HasColumnType("datetime")
                        .HasColumnName("MPCommnetDate");

                    b.Property<string>("MpostId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("MPostID");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("MpcommentId");

                    b.HasIndex("MpostId");

                    b.HasIndex("UserId");

                    b.ToTable("MediaPostComments");
                });

            modelBuilder.Entity("AreaBox_V0._1.Data.Model.MediaPostLikes", b =>
                {
                    b.Property<string>("MpostId")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("MPostID");

                    b.Property<string>("UserId")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("UserID");

                    b.HasKey("MpostId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("MediaPostLikes");
                });

            modelBuilder.Entity("AreaBox_V0._1.Data.Model.MediaPosts", b =>
                {
                    b.Property<string>("MpostId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("MPostID");

                    b.Property<int>("MpcategoryId")
                        .HasColumnType("int")
                        .HasColumnName("MPCategoryID");

                    b.Property<int>("MpcityId")
                        .HasColumnType("int")
                        .HasColumnName("MPCityID");

                    b.Property<DateTime>("Mpdate")
                        .HasColumnType("datetime")
                        .HasColumnName("MPDate");

                    b.Property<string>("Mpimage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("MPImage");

                    b.Property<string>("MplongDescription")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("MPLongDescription");

                    b.Property<string>("MpshortDescription")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("MPShortDescription");

                    b.Property<bool>("Mpstate")
                        .HasColumnType("bit")
                        .HasColumnName("MPState");

                    b.Property<string>("MpuserId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("MPUserID");

                    b.HasKey("MpostId");

                    b.HasIndex("MpcategoryId");

                    b.HasIndex("MpcityId");

                    b.HasIndex("MpuserId");

                    b.ToTable("MediaPosts");
                });

            modelBuilder.Entity("AreaBox_V0._1.Data.Model.MediaPostsReports", b =>
                {
                    b.Property<string>("MpostId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("MPostID");

                    b.Property<int>("PostReportId")
                        .HasColumnType("int")
                        .HasColumnName("PostReportID");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("MpostId", "PostReportId");

                    b.HasIndex("UserId");

                    b.HasIndex(new[] { "MpostId" }, "IX_MediaPostsReports_MPostID");

                    b.HasIndex(new[] { "PostReportId" }, "IX_MediaPostsReports_PostReportID");

                    b.ToTable("MediaPostsReports");
                });

            modelBuilder.Entity("AreaBox_V0._1.Data.Model.PostReports", b =>
                {
                    b.Property<int>("PostReportId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PostReportId"));

                    b.Property<int>("PostTypeId")
                        .HasColumnType("int");

                    b.Property<int>("ReportTypeId")
                        .HasColumnType("int")
                        .HasColumnName("ReportTypeID");

                    b.HasKey("PostReportId");

                    b.HasIndex("PostTypeId");

                    b.HasIndex("ReportTypeId");

                    b.ToTable("PostReports");
                });

            modelBuilder.Entity("AreaBox_V0._1.Data.Model.PostType", b =>
                {
                    b.Property<int>("PostTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PostTypeId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PostTypeId");

                    b.ToTable("PostTypes");
                });

            modelBuilder.Entity("AreaBox_V0._1.Data.Model.QuestionPostComments", b =>
                {
                    b.Property<string>("QpcommentId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("QPCommentID");

                    b.Property<string>("QpcommentContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("QPCommentContent");

                    b.Property<DateTime>("QpcommentDate")
                        .HasColumnType("datetime")
                        .HasColumnName("QPCommentDate");

                    b.Property<string>("QpostId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("QPostID");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("QpcommentId");

                    b.HasIndex("QpostId");

                    b.HasIndex("UserId");

                    b.ToTable("QuestionPostComments");
                });

            modelBuilder.Entity("AreaBox_V0._1.Data.Model.QuestionPosts", b =>
                {
                    b.Property<string>("QpostId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("QPostID");

                    b.Property<int>("QpcategoryId")
                        .HasColumnType("int")
                        .HasColumnName("QPCategoryID");

                    b.Property<int>("QpcityId")
                        .HasColumnType("int")
                        .HasColumnName("QPCityID");

                    b.Property<DateTime>("Qpdate")
                        .HasColumnType("datetime")
                        .HasColumnName("QPDate");

                    b.Property<string>("Qpdescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("QPDescription");

                    b.Property<bool>("Qpstate")
                        .HasColumnType("bit")
                        .HasColumnName("QPState");

                    b.Property<string>("Qptitle")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("QPTitle");

                    b.Property<string>("QpuserId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("QPUserID");

                    b.HasKey("QpostId");

                    b.HasIndex("QpcategoryId");

                    b.HasIndex("QpcityId");

                    b.HasIndex("QpuserId");

                    b.ToTable("QuestionPosts");
                });

            modelBuilder.Entity("AreaBox_V0._1.Data.Model.QuestionPostsReports", b =>
                {
                    b.Property<string>("QpostId")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("QPostID");

                    b.Property<int>("PostReportId")
                        .HasColumnType("int")
                        .HasColumnName("PostReportID");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("QpostId", "PostReportId");

                    b.HasIndex("PostReportId");

                    b.HasIndex("UserId");

                    b.ToTable("QuestionPostsReports");
                });

            modelBuilder.Entity("AreaBox_V0._1.Data.Model.ReportTypes", b =>
                {
                    b.Property<int>("ReportTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReportTypeId"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ReportTypeId");

                    b.ToTable("ReportTypes");
                });

            modelBuilder.Entity("AreaBox_V0._1.Data.Model.TechnicalReports", b =>
                {
                    b.Property<int>("TechnicalReportId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("TechnicalReportID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TechnicalReportId"));

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("UserID");

                    b.HasKey("TechnicalReportId");

                    b.HasIndex("UserId");

                    b.ToTable("TechnicalReports");
                });

            modelBuilder.Entity("AreaBox_V0._1.Data.Model.UserCategories", b =>
                {
                    b.Property<string>("UserId")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("UserID");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("UserCategories");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("AreaBox_V0._1.Data.Model.Cities", b =>
                {
                    b.HasOne("AreaBox_V0._1.Data.Model.Countries", "Country")
                        .WithMany("Cities")
                        .HasForeignKey("CountryId")
                        .IsRequired()
                        .HasConstraintName("FK_Cities_Countries");

                    b.Navigation("Country");
                });

            modelBuilder.Entity("AreaBox_V0._1.Data.Model.MediaPostComments", b =>
                {
                    b.HasOne("AreaBox_V0._1.Data.Model.MediaPosts", "Mpost")
                        .WithMany("MediaPostComments")
                        .HasForeignKey("MpostId")
                        .IsRequired()
                        .HasConstraintName("FK_MediaPostComments_MediaPosts");

                    b.HasOne("AreaBox_V0._1.Data.Model.ApplicationUser", "User")
                        .WithMany("MediaPostComments")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_MediaPostComments_Users");

                    b.Navigation("Mpost");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AreaBox_V0._1.Data.Model.MediaPostLikes", b =>
                {
                    b.HasOne("AreaBox_V0._1.Data.Model.MediaPosts", "Mpost")
                        .WithMany("MediaPostsLikes")
                        .HasForeignKey("MpostId")
                        .IsRequired()
                        .HasConstraintName("FK_MediaPostLikes_MediaPosts");

                    b.HasOne("AreaBox_V0._1.Data.Model.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_MediaPostLikes_AspNetUsers");

                    b.Navigation("Mpost");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AreaBox_V0._1.Data.Model.MediaPosts", b =>
                {
                    b.HasOne("AreaBox_V0._1.Data.Model.Categories", "Mpcategory")
                        .WithMany("MediaPosts")
                        .HasForeignKey("MpcategoryId")
                        .IsRequired()
                        .HasConstraintName("FK_MediaPosts_Categories");

                    b.HasOne("AreaBox_V0._1.Data.Model.Cities", "Mpcity")
                        .WithMany("MediaPosts")
                        .HasForeignKey("MpcityId")
                        .IsRequired()
                        .HasConstraintName("FK_MediaPosts_Cities");

                    b.HasOne("AreaBox_V0._1.Data.Model.ApplicationUser", "Mpuser")
                        .WithMany("MediaPosts")
                        .HasForeignKey("MpuserId")
                        .IsRequired()
                        .HasConstraintName("FK_MediaPosts_AspNetUsers");

                    b.Navigation("Mpcategory");

                    b.Navigation("Mpcity");

                    b.Navigation("Mpuser");
                });

            modelBuilder.Entity("AreaBox_V0._1.Data.Model.MediaPostsReports", b =>
                {
                    b.HasOne("AreaBox_V0._1.Data.Model.MediaPosts", "Mpost")
                        .WithMany()
                        .HasForeignKey("MpostId")
                        .IsRequired()
                        .HasConstraintName("FK_MediaPostsReports_MediaPosts");

                    b.HasOne("AreaBox_V0._1.Data.Model.PostReports", "PostReport")
                        .WithMany()
                        .HasForeignKey("PostReportId")
                        .IsRequired()
                        .HasConstraintName("FK_MediaPostsReports_ReportTypes");

                    b.HasOne("AreaBox_V0._1.Data.Model.ApplicationUser", "User")
                        .WithMany("MediaPostsReports")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_MediaPostsReports_Users");

                    b.Navigation("Mpost");

                    b.Navigation("PostReport");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AreaBox_V0._1.Data.Model.PostReports", b =>
                {
                    b.HasOne("AreaBox_V0._1.Data.Model.PostType", "PostType")
                        .WithMany("PostReports")
                        .HasForeignKey("PostTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AreaBox_V0._1.Data.Model.ReportTypes", "ReportTypes")
                        .WithMany("PostReports")
                        .HasForeignKey("ReportTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PostType");

                    b.Navigation("ReportTypes");
                });

            modelBuilder.Entity("AreaBox_V0._1.Data.Model.QuestionPostComments", b =>
                {
                    b.HasOne("AreaBox_V0._1.Data.Model.QuestionPosts", "Qpost")
                        .WithMany("QuestionPostComments")
                        .HasForeignKey("QpostId")
                        .IsRequired()
                        .HasConstraintName("FK_QuestionPostComments_QuestionPosts");

                    b.HasOne("AreaBox_V0._1.Data.Model.ApplicationUser", "User")
                        .WithMany("QuestionPostComments")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_QuestionPostComments_Users");

                    b.Navigation("Qpost");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AreaBox_V0._1.Data.Model.QuestionPosts", b =>
                {
                    b.HasOne("AreaBox_V0._1.Data.Model.Categories", "Qpcategory")
                        .WithMany("QuestionPosts")
                        .HasForeignKey("QpcategoryId")
                        .IsRequired()
                        .HasConstraintName("FK_QuestionPosts_Categories");

                    b.HasOne("AreaBox_V0._1.Data.Model.Cities", "Qpcity")
                        .WithMany("QuestionPosts")
                        .HasForeignKey("QpcityId")
                        .IsRequired()
                        .HasConstraintName("FK_QuestionPosts_Cities");

                    b.HasOne("AreaBox_V0._1.Data.Model.ApplicationUser", "Qpuser")
                        .WithMany("QuestionPosts")
                        .HasForeignKey("QpuserId")
                        .IsRequired()
                        .HasConstraintName("FK_QuestionPosts_AspNetUsers");

                    b.Navigation("Qpcategory");

                    b.Navigation("Qpcity");

                    b.Navigation("Qpuser");
                });

            modelBuilder.Entity("AreaBox_V0._1.Data.Model.QuestionPostsReports", b =>
                {
                    b.HasOne("AreaBox_V0._1.Data.Model.PostReports", "PostReports")
                        .WithMany()
                        .HasForeignKey("PostReportId")
                        .IsRequired()
                        .HasConstraintName("FK_QuestionPostsReports_PostReports");

                    b.HasOne("AreaBox_V0._1.Data.Model.QuestionPosts", "Qpost")
                        .WithMany()
                        .HasForeignKey("QpostId")
                        .IsRequired()
                        .HasConstraintName("FK_QuestionPostsReports_QuestionPosts");

                    b.HasOne("AreaBox_V0._1.Data.Model.ApplicationUser", "User")
                        .WithMany("QuestionPostsReports")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_QuestionPostsReports_Users");

                    b.Navigation("PostReports");

                    b.Navigation("Qpost");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AreaBox_V0._1.Data.Model.TechnicalReports", b =>
                {
                    b.HasOne("AreaBox_V0._1.Data.Model.ApplicationUser", "User")
                        .WithMany("TechnicalReports")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_TechnicalReports_AspNetUsers");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AreaBox_V0._1.Data.Model.UserCategories", b =>
                {
                    b.HasOne("AreaBox_V0._1.Data.Model.Categories", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .IsRequired()
                        .HasConstraintName("FK_UserCategories_Category");

                    b.HasOne("AreaBox_V0._1.Data.Model.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_UserCategories_AspNetUsers");

                    b.Navigation("Category");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("AreaBox_V0._1.Data.Model.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("AreaBox_V0._1.Data.Model.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AreaBox_V0._1.Data.Model.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("AreaBox_V0._1.Data.Model.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AreaBox_V0._1.Data.Model.ApplicationUser", b =>
                {
                    b.Navigation("MediaPostComments");

                    b.Navigation("MediaPosts");

                    b.Navigation("MediaPostsReports");

                    b.Navigation("QuestionPostComments");

                    b.Navigation("QuestionPosts");

                    b.Navigation("QuestionPostsReports");

                    b.Navigation("TechnicalReports");
                });

            modelBuilder.Entity("AreaBox_V0._1.Data.Model.Categories", b =>
                {
                    b.Navigation("MediaPosts");

                    b.Navigation("QuestionPosts");
                });

            modelBuilder.Entity("AreaBox_V0._1.Data.Model.Cities", b =>
                {
                    b.Navigation("MediaPosts");

                    b.Navigation("QuestionPosts");
                });

            modelBuilder.Entity("AreaBox_V0._1.Data.Model.Countries", b =>
                {
                    b.Navigation("Cities");
                });

            modelBuilder.Entity("AreaBox_V0._1.Data.Model.MediaPosts", b =>
                {
                    b.Navigation("MediaPostComments");

                    b.Navigation("MediaPostsLikes");
                });

            modelBuilder.Entity("AreaBox_V0._1.Data.Model.PostType", b =>
                {
                    b.Navigation("PostReports");
                });

            modelBuilder.Entity("AreaBox_V0._1.Data.Model.QuestionPosts", b =>
                {
                    b.Navigation("QuestionPostComments");
                });

            modelBuilder.Entity("AreaBox_V0._1.Data.Model.ReportTypes", b =>
                {
                    b.Navigation("PostReports");
                });
#pragma warning restore 612, 618
        }
    }
}
