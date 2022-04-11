using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Trace.Repository
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<TripArticle> TripArticles { get; set; }
        public virtual DbSet<TripEvent> TripEvents { get; set; }
        public virtual DbSet<TripGroup> TripGroups { get; set; }
        public virtual DbSet<TripPhoto> TripPhotos { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserFriend> UserFriends { get; set; }
        public virtual DbSet<UserRecord> UserRecords { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Chinese_Taiwan_Stroke_CI_AS");

            modelBuilder.Entity<TripArticle>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK_Article");

                entity.Property(e => e.RecordId).ValueGeneratedNever();
            });

            modelBuilder.Entity<TripGroup>(entity =>
            {
                entity.HasKey(e => new { e.TripId, e.MemberId });
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserAccount).IsUnicode(false);

                entity.Property(e => e.UserPassword).IsUnicode(false);
            });

            modelBuilder.Entity<UserFriend>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.FriendId });
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
