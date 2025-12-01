using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using OtoMangaStore.Domain.Models;

namespace OtoMangaStore.Infrastructure.Persistence
{
    public class OtoDbContext : IdentityDbContext<ApplicationUser>
    {
        public OtoDbContext(DbContextOptions<OtoDbContext> options) 
            : base(options) 
        {
        }
        public DbSet<Content> Mangas { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderItem> OrderItems { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Author> Authors { get; set; } = null!;
        public DbSet<PriceHistory> PriceHistories { get; set; } = null!;
        public DbSet<ClickMetric> ClickMetrics { get; set; } = null!;  

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 
            modelBuilder.Entity<Content>().ToTable("content"); 
            
            modelBuilder.Entity<Order>().ToTable("orders");
            modelBuilder.Entity<OrderItem>().ToTable("orderitems");

            modelBuilder.Entity<Category>().ToTable("categories");
            modelBuilder.Entity<Author>().ToTable("authors");
            modelBuilder.Entity<PriceHistory>().ToTable("price_history");
            modelBuilder.Entity<ClickMetric>().ToTable("click_metrics");
            
            modelBuilder.Entity<PriceHistory>()
                .HasOne(ph => ph.Content)
                .WithMany()
                .HasForeignKey(ph => ph.MangaId);
            
            modelBuilder.Entity<ClickMetric>()
                .HasOne(cm => cm.Content)
                .WithMany() 
                .HasForeignKey(cm => cm.MangaId);

        }
    }
}
