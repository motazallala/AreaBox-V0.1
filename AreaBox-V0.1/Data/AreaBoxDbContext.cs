﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AreaBox_V0._1.Data.Model;

public class AreaBoxDbContext : IdentityDbContext<ApplicationUser>
{
    public AreaBoxDbContext(DbContextOptions<AreaBoxDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categories> Categories { get; set; }
    public virtual DbSet<UserCategories> UserCategories { get; set; }
    public virtual DbSet<UserMediaPostSave> UserMediaPosts { get; set; }
    public virtual DbSet<UserQuestionPostSave> UserQuestionPosts { get; set; }

    public virtual DbSet<Cities> Cities { get; set; }

    public virtual DbSet<Countries> Countries { get; set; }

    public virtual DbSet<MediaPostComments> MediaPostComments { get; set; }

    public virtual DbSet<MediaPostLikes> MediaPostLikes { get; set; }

    public virtual DbSet<MediaPosts> MediaPosts { get; set; }

    public virtual DbSet<MediaPostsReports> MediaPostsReports { get; set; }

    public virtual DbSet<QuestionPostComments> QuestionPostComments { get; set; }

    public virtual DbSet<QuestionPosts> QuestionPosts { get; set; }

    public virtual DbSet<QuestionPostsReports> QuestionPostsReports { get; set; }

    public virtual DbSet<ReportTypes> ReportTypes { get; set; }

    public virtual DbSet<PostType> PostTypes { get; set; }

    public virtual DbSet<PostReports> PostReports { get; set; }

    public virtual DbSet<TechnicalReports> TechnicalReports { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApplicationUser>(entity =>
        {
            entity.Property(u => u.FirstName).HasMaxLength(255);
            entity.Property(u => u.LastName).HasMaxLength(255);
            entity.Property(u => u.Gender).HasMaxLength(20);
            entity.Property(u => u.DOB).HasColumnType("Date");
            entity.Property(u => u.Bio).HasMaxLength(450);
            entity.Property(u => u.ProfilePicture).HasColumnType("nvarchar(max)");
        });

        modelBuilder.Entity<Categories>(entity =>
        {
            entity.HasKey(e => e.CategoryId);

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName)
                .IsRequired()
                .HasMaxLength(100);
        });

        modelBuilder.Entity<Cities>(entity =>
        {
            entity.HasKey(e => e.CitryId);

            entity.Property(e => e.CitryId).HasColumnName("CitryID");
            entity.Property(e => e.CityName)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.CountryId).HasColumnName("CountryID");

            entity.HasOne(d => d.Country).WithMany(p => p.Cities)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cities_Countries");
        });

        modelBuilder.Entity<Countries>(entity =>
        {
            entity.HasKey(e => e.CountryId);

            entity.Property(e => e.CountryId).HasColumnName("CountryID");
            entity.Property(e => e.CountryName)
                .IsRequired()
                .HasMaxLength(100);
        });

        modelBuilder.Entity<MediaPostComments>(entity =>
        {
            entity.HasKey(e => e.MpcommentId);

            entity.Property(e => e.MpcommentId).HasColumnName("MPCommentID");
            entity.Property(e => e.MpcommentContent)
                .IsRequired()
                .HasColumnName("MPCommentContent");
            entity.Property(e => e.MpcommnetDate)
                .HasColumnType("datetime")
                .HasColumnName("MPCommnetDate");
            entity.Property(e => e.MpostId)
                .IsRequired()
                .HasMaxLength(450)
                .HasColumnName("MPostID");

            entity.HasOne(d => d.Mpost).WithMany(p => p.MediaPostComments)
                .HasForeignKey(d => d.MpostId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_MediaPostComments_MediaPosts");

            entity.HasOne(d => d.User).WithMany(p => p.MediaPostComments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MediaPostComments_Users");
        });
        modelBuilder.Entity<UserCategories>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.CategoryId});

            entity.Property(e => e.CategoryId)
                .IsRequired();
            entity.Property(e => e.UserId)
                .IsRequired()
                .HasMaxLength(450)
                .HasColumnName("UserID");

            entity.HasOne(d => d.Category).WithMany()
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserCategories_Category");

            entity.HasOne(d => d.User).WithMany()
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserCategories_AspNetUsers");
        });


        modelBuilder.Entity<UserMediaPostSave>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.MpostId });

            entity.Property(e => e.MpostId)
                .IsRequired();
            entity.Property(e => e.UserId)
                .IsRequired()
                .HasMaxLength(450)
                .HasColumnName("UserID");

            entity.HasOne(d => d.MediaPosts).WithMany()
                .HasForeignKey(d => d.MpostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserMediaPostSave_MediaPosts");

            entity.HasOne(d => d.User).WithMany()
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserMediaPostSave_AspNetUsers");
        });

        modelBuilder.Entity<UserQuestionPostSave>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.QpostId });

            entity.Property(e => e.QpostId)
                .IsRequired();
            entity.Property(e => e.UserId)
                .IsRequired()
                .HasMaxLength(450)
                .HasColumnName("UserID");

            entity.HasOne(d => d.QuestionPosts).WithMany()
                .HasForeignKey(d => d.QpostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserQuestionPostSave_QuestionPosts");

            entity.HasOne(d => d.User).WithMany()
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserQuestionPostSave_AspNetUsers");
        });

        modelBuilder.Entity<MediaPostLikes>(entity =>
        {
            entity.HasKey(e => new { e.MpostId , e.UserId});

            entity.Property(e => e.MpostId)
                .IsRequired()
                .HasMaxLength(450)
                .HasColumnName("MPostID");
            entity.Property(e => e.UserId)
                .IsRequired()
                .HasMaxLength(450)
                .HasColumnName("UserID");

            entity.HasOne(d => d.Mpost).WithMany(l => l.MediaPostsLikes)
                .HasForeignKey(d => d.MpostId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_MediaPostLikes_MediaPosts");

            entity.HasOne(d => d.User).WithMany()
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MediaPostLikes_AspNetUsers");
        });

        modelBuilder.Entity<MediaPosts>(entity =>
        {
            entity.HasKey(e => e.MpostId);

            entity.Property(e => e.MpostId).HasColumnName("MPostID");
            entity.Property(e => e.MpcategoryId).HasColumnName("MPCategoryID");
            entity.Property(e => e.MpcityId).HasColumnName("MPCityID");
            entity.Property(e => e.Mpdate)
                .HasColumnType("datetime")
                .HasColumnName("MPDate");
            entity.Property(e => e.Mpimage)
                .IsRequired()
                .HasColumnName("MPImage");
            entity.Property(e => e.MplongDescription)
                .IsRequired()
                .HasMaxLength(450)
                .HasColumnName("MPLongDescription");
            entity.Property(e => e.MpshortDescription)
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnName("MPShortDescription");
            entity.Property(e => e.Mpstate).HasColumnName("MPState");
            entity.Property(e => e.MpuserId)
                .IsRequired()
                .HasMaxLength(450)
                .HasColumnName("MPUserID");



            entity.HasOne(d => d.Mpcategory).WithMany(p => p.MediaPosts)
                .HasForeignKey(d => d.MpcategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MediaPosts_Categories");

            entity.HasOne(d => d.Mpcity).WithMany(p => p.MediaPosts)
                .HasForeignKey(d => d.MpcityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MediaPosts_Cities");

            entity.HasOne(d => d.Mpuser).WithMany(p => p.MediaPosts)
                .HasForeignKey(d => d.MpuserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MediaPosts_AspNetUsers");
        });

        modelBuilder.Entity<MediaPostsReports>(entity =>
        {
            entity.HasKey(e => new { e.MpostId, e.PostReportId });

            entity.HasIndex(e => e.MpostId, "IX_MediaPostsReports_MPostID");

            entity.HasIndex(e => e.PostReportId, "IX_MediaPostsReports_PostReportID");

            entity.Property(e => e.MpostId)
                .IsRequired()
                .HasColumnName("MPostID");
            entity.Property(e => e.PostReportId).HasColumnName("PostReportID");



            entity.HasOne(d => d.Mpost).WithMany(p => p.MediaPostsReports)
                .HasForeignKey(d => d.MpostId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_MediaPostsReports_MediaPosts");

            entity.HasOne(d => d.PostReport).WithOne()
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_MediaPostsReports_ReportTypes");
            entity.HasOne(d => d.User).WithMany(d => d.MediaPostsReports)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MediaPostsReports_Users");
        });

        modelBuilder.Entity<QuestionPostComments>(entity =>
        {
            entity.HasKey(e => e.QpcommentId);

            entity.Property(e => e.QpcommentId).HasColumnName("QPCommentID");
            entity.Property(e => e.QpcommentContent)
                .IsRequired()
                .HasColumnName("QPCommentContent");
            entity.Property(e => e.QpcommentDate)
                .HasColumnType("datetime")
                .HasColumnName("QPCommentDate");
            entity.Property(e => e.QpostId)
                .IsRequired()
                .HasMaxLength(450)
                .HasColumnName("QPostID");

            entity.HasOne(d => d.Qpost).WithMany(p => p.QuestionPostComments)
                .HasForeignKey(d => d.QpostId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_QuestionPostComments_QuestionPosts");

            entity.HasOne(d => d.User).WithMany(p => p.QuestionPostComments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QuestionPostComments_Users");
        });

        modelBuilder.Entity<QuestionPosts>(entity =>
        {
            entity.HasKey(e => e.QpostId);

            entity.Property(e => e.QpostId).HasColumnName("QPostID");
            entity.Property(e => e.QpcategoryId).HasColumnName("QPCategoryID");
            entity.Property(e => e.QpcityId).HasColumnName("QPCityID");
            entity.Property(e => e.Qpdate)
                .HasColumnType("datetime")
                .HasColumnName("QPDate");
            entity.Property(e => e.Qpdescription)
                .IsRequired()
                .HasColumnName("QPDescription");
            entity.Property(e => e.Qpstate).HasColumnName("QPState");
            entity.Property(e => e.Qptitle)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("QPTitle");
            entity.Property(e => e.QpuserId)
                .IsRequired()
                .HasMaxLength(450)
                .HasColumnName("QPUserID");

            entity.HasOne(d => d.Qpcategory).WithMany(p => p.QuestionPosts)
                .HasForeignKey(d => d.QpcategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QuestionPosts_Categories");

            entity.HasOne(d => d.Qpcity).WithMany(p => p.QuestionPosts)
                .HasForeignKey(d => d.QpcityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QuestionPosts_Cities");

            entity.HasOne(d => d.Qpuser).WithMany(p => p.QuestionPosts)
                .HasForeignKey(d => d.QpuserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QuestionPosts_AspNetUsers");
        });

        modelBuilder.Entity<QuestionPostsReports>(entity =>
        {
            entity.HasKey(e => new { e.QpostId, e.PostReportId });

            entity.Property(e => e.QpostId)
                .IsRequired()
                .HasMaxLength(450)
                .HasColumnName("QPostID");
            entity.Property(e => e.PostReportId).HasColumnName("PostReportID");

            entity.HasOne(d => d.Qpost).WithMany()
                .HasForeignKey(d => d.QpostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QuestionPostsReports_QuestionPosts");

            entity.HasOne(d => d.PostReports).WithMany()
                .HasForeignKey(d => d.PostReportId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QuestionPostsReports_PostReports");
            entity.HasOne(d => d.User).WithMany(d => d.QuestionPostsReports)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QuestionPostsReports_Users");
        });

        modelBuilder.Entity<PostReports>(entity =>
        {
            entity.HasKey(e => e.PostReportId);

            entity.Property(e => e.ReportTypeId).HasColumnName("ReportTypeID");
            entity.HasOne(d => d.PostType).WithMany(d => d.PostReports)
            .HasForeignKey(d => d.PostTypeId);
            entity.HasOne(d => d.ReportTypes).WithMany(d => d.PostReports)
            .HasForeignKey(d => d.ReportTypeId);
        });

        modelBuilder.Entity<PostType>(entity =>
        {
            entity.HasKey(e => e.PostTypeId);
        });

        modelBuilder.Entity<ReportTypes>(entity =>
        {
            entity.HasKey(e => e.ReportTypeId);
        });

        modelBuilder.Entity<TechnicalReports>(entity =>
        {
            entity.HasKey(e => e.TechnicalReportId);

            entity.Property(e => e.TechnicalReportId).HasColumnName("TechnicalReportID");
            entity.Property(e => e.Details).IsRequired();
            entity.Property(e => e.Type)
                .IsRequired()
                .HasMaxLength(150);
            entity.Property(e => e.UserEmail).HasMaxLength(450);
            entity.Property(e => e.UserId)
                .IsRequired()
                .HasMaxLength(450)
                .HasColumnName("UserID");


            entity.Property(e => e.SuperAdminId)
                .HasMaxLength(450)
                .HasColumnName("SuperAdminId");

            entity.Property(e => e.TechnicalAdminId)
                .HasMaxLength(450)
                .HasColumnName("TechnicalAdminId");

            entity.Property(e => e.ReviewNote)
                .HasMaxLength(450)
                .HasColumnName("ReviewNote");

            entity.HasOne(d => d.User).WithMany(p => p.TechnicalReports)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TechnicalReports_AspNetUsers");

            // Relationship with the technical admin
            entity.HasOne(d => d.TechnicalAdmin)
                .WithMany()
                .HasForeignKey(d => d.TechnicalAdminId)
                .HasConstraintName("FK_TechnicalReports_TechnicalAdmin")
                .OnDelete(DeleteBehavior.Restrict);

            // Relationship with the super admin
            entity.HasOne(d => d.SuperAdmin)
                .WithMany()
                .HasForeignKey(d => d.SuperAdminId)
                .HasConstraintName("FK_TechnicalReports_SuperAdmin")
                .OnDelete(DeleteBehavior.Restrict);
        });





        base.OnModelCreating(modelBuilder);
    }
}