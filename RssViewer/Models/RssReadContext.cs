using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RssViewer.Models
{
    public partial class RssReadContext : DbContext
    {
        public RssReadContext()
        {
        }

        public RssReadContext(DbContextOptions<RssReadContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MigrationHistory> MigrationHistory { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Source> Sources { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Initial Catalog=RssReader;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.1-servicing-10028");

            modelBuilder.Entity<MigrationHistory>(entity =>
            {
                entity.HasKey(e => new { e.MigrationId, e.ContextKey })
                    .HasName("PK_dbo.__MigrationHistory");

                entity.ToTable("__MigrationHistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ContextKey).HasMaxLength(300);

                entity.Property(e => e.Model).IsRequired();

                entity.Property(e => e.ProductVersion)
                    .IsRequired()
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<News>(entity =>
            {
                entity.HasIndex(e => e.SourceId)
                    .HasName("IX_SourceId");

                entity.Property(e => e.PublicationDate).HasColumnType("datetime");

                entity.HasOne(d => d.Source)
                    .WithMany(p => p.News)
                    .HasForeignKey(d => d.SourceId)
                    .HasConstraintName("FK_dbo.News_dbo.Sources_SourceId");
            });

            modelBuilder.Entity<Source>(entity =>
            {
                entity.HasKey(e => e.SourceId)
                    .HasName("PK_dbo.Sources");
            });
        }
    }
}
