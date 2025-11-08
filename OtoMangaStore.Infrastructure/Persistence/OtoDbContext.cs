using Microsoft.EntityFrameworkCore;
using OtoMangaStore.Domain.Models;

namespace OtoMangaStore.Infrastructure.Persistence
{
    public class OtoDbContext : DbContext
    {
        public OtoDbContext(DbContextOptions<OtoDbContext> options) 
            : base(options)
        {
        }
        
        public DbSet<Manga> Mangas { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Manga>().ToTable("mangas");
            modelBuilder.Entity<Order>().ToTable("orders");
            modelBuilder.Entity<OrderItem>().ToTable("orderitems");
            
        }
    }
}