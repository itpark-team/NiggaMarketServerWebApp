using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using NiggaMarketServerWebApp.Models;

namespace NiggaMarketServerWebApp.DbConnector;

public partial class NiggamarketDbContext : DbContext
{
    public NiggamarketDbContext()
    {
    }

    public NiggamarketDbContext(DbContextOptions<NiggamarketDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Announcement> Announcements { get; set; }

    public virtual DbSet<AnnouncementWork> AnnouncementsWorks { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Injury> Injuries { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Work> Works { get; set; }

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //     => optionsBuilder.UseNpgsql("Host=194.67.105.79:5432;Database=niggamarket_db;Username=niggamarket_user;Password=12345");
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=postgres:5432;Database=postgres;Username=postgres;Password=postgres");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Announcement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("announcements_pk");

            entity.ToTable("announcements");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(2048)
                .HasColumnName("description");
            entity.Property(e => e.Gender).HasColumnName("gender");
            entity.Property(e => e.Height).HasColumnName("height");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(32)
                .HasColumnName("name");
            entity.Property(e => e.PhoneNumber)
                .IsRequired()
                .HasMaxLength(32)
                .HasColumnName("phone_number");
            entity.Property(e => e.PicturePath)
                .IsRequired()
                .HasMaxLength(1024)
                .HasColumnName("picture_path");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Weight).HasColumnName("weight");

            entity.HasOne(d => d.User).WithMany(p => p.Announcements)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("announcements_users_id_fk");

            entity.HasMany(d => d.Injuries).WithMany(p => p.Announcements)
                .UsingEntity<Dictionary<string, object>>(
                    "AnnouncementsInjury",
                    r => r.HasOne<Injury>().WithMany()
                        .HasForeignKey("InjuryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("announcements_injuries_injuries_id_fk"),
                    l => l.HasOne<Announcement>().WithMany()
                        .HasForeignKey("AnnouncementId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("announcements_injuries_announcements_id_fk"),
                    j =>
                    {
                        j.HasKey("AnnouncementId", "InjuryId").HasName("announcements_injuries_pk");
                        j.ToTable("announcements_injuries");
                        j.IndexerProperty<int>("AnnouncementId").HasColumnName("announcement_id");
                        j.IndexerProperty<int>("InjuryId").HasColumnName("injury_id");
                    });
        });

        modelBuilder.Entity<AnnouncementWork>(entity =>
        {
            entity.HasKey(e => new { e.AnnouncementId, e.WorkId }).HasName("announcements_works_pk");

            entity.ToTable("announcements_works");

            entity.Property(e => e.AnnouncementId).HasColumnName("announcement_id");
            entity.Property(e => e.WorkId).HasColumnName("work_id");
            entity.Property(e => e.Experience).HasColumnName("experience");

            entity.HasOne(d => d.Announcement).WithMany(p => p.AnnouncementsWorks)
                .HasForeignKey(d => d.AnnouncementId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("announcements_works_announcements_id_fk");

            entity.HasOne(d => d.Work).WithMany(p => p.AnnouncementsWorks)
                .HasForeignKey(d => d.WorkId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("announcements_works_works_id_fk");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("comments_pk");

            entity.ToTable("comments");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AnnouncementId).HasColumnName("announcement_id");
            entity.Property(e => e.AuthorName)
                .IsRequired()
                .HasMaxLength(128)
                .HasColumnName("author_name");
            entity.Property(e => e.Content)
                .IsRequired()
                .HasMaxLength(2048)
                .HasColumnName("content");

            entity.HasOne(d => d.Announcement).WithMany(p => p.Comments)
                .HasForeignKey(d => d.AnnouncementId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("comments_announcements_id_fk");
        });

        modelBuilder.Entity<Injury>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("injuries_pk");

            entity.ToTable("injuries");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(128)
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pk");

            entity.ToTable("users");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(32)
                .HasColumnName("name");
            entity.Property(e => e.PhoneNumber)
                .IsRequired()
                .HasMaxLength(32)
                .HasColumnName("phone_number");
        });

        modelBuilder.Entity<Work>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("works_pk");

            entity.ToTable("works");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(128)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
