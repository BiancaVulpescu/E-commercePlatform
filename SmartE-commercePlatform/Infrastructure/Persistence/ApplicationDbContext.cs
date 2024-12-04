using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        private const string UuidGenerationFunction = "uuid_generate_v4()";

        public DbSet<Product> Products { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp");
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnType("uuid")
                    .HasDefaultValueSql(UuidGenerationFunction)
                    .ValueGeneratedOnAdd();
                entity.Property(e => e.Title).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Description).HasMaxLength(1000);
                entity.Property(e => e.Price).IsRequired();
            });
            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("Clients");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                .HasColumnType("uuid")
                .HasDefaultValueSql(UuidGenerationFunction)
                .ValueGeneratedOnAdd();
                entity.Property(e => e.Email).IsRequired().HasMaxLength(30);
                entity.Property(e => e.Username).IsRequired().HasMaxLength(30);
                entity.Property(e => e.Password).IsRequired();
                entity.Property(e => e.Location).IsRequired();
            });
            modelBuilder.Entity<Wishlist>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnType("uuid")
                    .HasDefaultValueSql(UuidGenerationFunction)
                    .ValueGeneratedOnAdd();
                entity.HasMany(e => e.Products)
                    .WithMany(e => e.Wishlists);
            });
            modelBuilder.Entity<ShoppingCart>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnType("uuid")
                    .HasDefaultValueSql(UuidGenerationFunction)
                    .ValueGeneratedOnAdd();
                entity.HasMany(e => e.Products)
                    .WithMany(e => e.ShoppingCarts)
                    .UsingEntity<ShoppingCartProduct>(/*TODO: j => j.Property(e => e.Quantity)*/);
             });

            
        }
    }
}
