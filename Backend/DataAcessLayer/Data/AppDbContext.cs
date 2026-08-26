using System;
using System.Collections.Generic;
using DataAccessLayer.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AuditLog> AuditLogs { get; set; }

    public virtual DbSet<CheckIn> CheckIns { get; set; }

    public virtual DbSet<Community> Communities { get; set; }

    public virtual DbSet<CommunityMember> CommunityMembers { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<EventReport> EventReports { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Registration> Registrations { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.HasKey(e => e.AuditLogId).HasName("PK__AuditLog__EB5F6CDDDEE01A3A");

            entity.Property(e => e.AuditLogId)
                .HasDefaultValueSql("(newsequentialid())")
                .HasColumnName("AuditLogID");
            entity.Property(e => e.Action)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.AtTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("atTime");
            entity.Property(e => e.EntityEfffected)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.AuditLogs)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AuditLogs__UserI__2FCF1A8A");
        });

        modelBuilder.Entity<CheckIn>(entity =>
        {
            entity.HasKey(e => e.CheckInId).HasName("PK__CheckIn__E64976A4A2D6151E");

            entity.ToTable("CheckIn");

            entity.HasIndex(e => e.RegistrationId, "UQ__CheckIn__6EF58831740B9AE8").IsUnique();

            entity.Property(e => e.CheckInId)
                .HasDefaultValueSql("(newsequentialid())")
                .HasColumnName("CheckInID");
            entity.Property(e => e.CheckedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.RegistrationId).HasColumnName("RegistrationID");

            entity.HasOne(d => d.Registration).WithOne(p => p.CheckIn)
                .HasForeignKey<CheckIn>(d => d.RegistrationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CheckIn__Registr__3587F3E0");

            entity.HasOne(d => d.VerifiedByNavigation).WithMany(p => p.CheckIns)
                .HasForeignKey(d => d.VerifiedBy)
                .HasConstraintName("FK__CheckIn__Verifie__367C1819");
        });

        modelBuilder.Entity<Community>(entity =>
        {
            entity.HasKey(e => e.CommunityId).HasName("PK__Communit__CCAA5B09C15913D6");

            entity.ToTable("Community");

            entity.HasIndex(e => e.Title, "idx_Community_Title");

            entity.Property(e => e.CommunityId)
                .HasDefaultValueSql("(newsequentialid())")
                .HasColumnName("CommunityID");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CommunityMember>(entity =>
        {
            entity.HasKey(e => e.CommunityMemberId).HasName("PK__Communit__F315FA36AC4D63FD");

            entity.ToTable("CommunityMember");

            entity.HasIndex(e => new { e.CommunityId, e.UserId }, "UQ__Communit__1DD2D7C21F9F5B30").IsUnique();

            entity.HasIndex(e => e.CommunityId, "idx_CommunityMember_CommuniyID");

            entity.HasIndex(e => e.UserId, "idx_CommunityMember_UserID");

            entity.Property(e => e.CommunityMemberId)
                .HasDefaultValueSql("(newsequentialid())")
                .HasColumnName("CommunityMemberID");
            entity.Property(e => e.CommunityId).HasColumnName("CommunityID");
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Community).WithMany(p => p.CommunityMembers)
                .HasForeignKey(d => d.CommunityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Community__Commu__00200768");

            entity.HasOne(d => d.User).WithMany(p => p.CommunityMembers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Community__UserI__01142BA1");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.EventId).HasName("PK__Event__7944C870A6E09999");

            entity.ToTable("Event");

            entity.Property(e => e.EventId)
                .HasDefaultValueSql("(newsequentialid())")
                .HasColumnName("EventID");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.CommunityId).HasColumnName("CommunityID");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.OrganizedId).HasColumnName("OrganizedID");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("startDate");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Draft");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("title");

            entity.HasOne(d => d.Community).WithMany(p => p.Events)
                .HasForeignKey(d => d.CommunityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Event__Community__08B54D69");

            entity.HasOne(d => d.Organized).WithMany(p => p.Events)
                .HasForeignKey(d => d.OrganizedId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Event__Organized__09A971A2");
        });

        modelBuilder.Entity<EventReport>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("EventReport");

            entity.Property(e => e.AttendancePercentage).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.AverageRating).HasColumnName("Average Rating");
            entity.Property(e => e.CurrentRegistrations).HasColumnName("Current Registrations");
            entity.Property(e => e.EventId).HasColumnName("EventID");
            entity.Property(e => e.EventTitle)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Event Title ");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PK__Feedback__6A4BEDF6E1652CF0");

            entity.ToTable("Feedback");

            entity.HasIndex(e => new { e.UserId, e.EventId }, "UQ__Feedback__001C802AFEC68FFA").IsUnique();

            entity.Property(e => e.FeedbackId)
                .HasDefaultValueSql("(newsequentialid())")
                .HasColumnName("FeedbackID");
            entity.Property(e => e.Comment)
                .IsUnicode(false)
                .HasColumnName("comment");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EventId).HasColumnName("EventID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Event).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Feedback__EventI__2A164134");

            entity.HasOne(d => d.User).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Feedback__UserID__2B0A656D");
        });

        modelBuilder.Entity<Registration>(entity =>
        {
            entity.HasKey(e => e.RegistrationId).HasName("PK__Registra__6EF588301C30AE1C");

            entity.ToTable("Registration");

            entity.HasIndex(e => new { e.MemberId, e.EventId }, "UQ_Registration_Unique_Active_Enrollment")
                .IsUnique()
                .HasFilter("([Status] IN ('Confirmed', 'Waitlisted'))");

            entity.Property(e => e.RegistrationId)
                .HasDefaultValueSql("(newsequentialid())")
                .HasColumnName("RegistrationID");
            entity.Property(e => e.EventId).HasColumnName("EventID");
            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.RequestedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TokenHash).HasMaxLength(32);

            entity.HasOne(d => d.Event).WithMany(p => p.Registrations)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Registrat__Event__18EBB532");

            entity.HasOne(d => d.Member).WithMany(p => p.Registrations)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Registrat__Membe__19DFD96B");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCACFC35EF81");

            entity.HasIndex(e => e.Email, "UQ__Users__AB6E616470D5F97D").IsUnique();

            entity.HasIndex(e => e.Fullname, "idx_Users_fullname");

            entity.Property(e => e.UserId)
                .HasDefaultValueSql("(newsequentialid())")
                .HasColumnName("UserID");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Fullname)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
