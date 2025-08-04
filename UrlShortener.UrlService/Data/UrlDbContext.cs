using Microsoft.EntityFrameworkCore;
using UrlShortener.Shared.Models.Entities;

namespace UrlShortener.UrlService.Data
{
    public class UrlDbContext : DbContext
    {
        public UrlDbContext(DbContextOptions<UrlDbContext> options) : base(options) { }

        public DbSet<ShortenedUrl> ShortenedUrls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShortenedUrl>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.ShortCode).IsUnique();
                entity.Property(e => e.ShortCode).HasMaxLength(10);
                entity.Property(e => e.OriginalUrl).HasMaxLength(2048);
                entity.Property(e => e.UserId).HasMaxLength(450);
            });
        }
    }
}
