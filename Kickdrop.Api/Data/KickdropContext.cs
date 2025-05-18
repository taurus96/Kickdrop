using Kickdrop.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Kickdrop.Api.Data
{
    public class KickdropContext : DbContext
    {
        public KickdropContext(DbContextOptions<KickdropContext> options) : base(options)
        {
        }

        public DbSet<Shoe> Shoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Shoe>().HasKey(s => s.Id);
            modelBuilder.Entity<Shoe>().Property(s => s.Brand).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Shoe>().Property(s => s.Size).IsRequired();
            modelBuilder.Entity<Shoe>().Property(s => s.Color).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Shoe>().Property(s => s.Price).IsRequired().HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Shoe>()
                .Property(s => s.Color)
                .HasConversion<string>();
        }
    }
}