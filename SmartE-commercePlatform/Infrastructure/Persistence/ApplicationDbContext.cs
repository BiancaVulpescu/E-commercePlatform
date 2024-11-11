using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        private const string UuidGenerationFunction = "uuid_generate_v4()";

        public DbSet<Product> Products { get; set; }
        public DbSet<WishlistItem> WishlistItems { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp");
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                .HasColumnType("uuid")
                .HasDefaultValueSql(UuidGenerationFunction)
                .ValueGeneratedOnAdd();
                entity.Property(e => e.Description).IsRequired().HasMaxLength(300);
                entity.Property(e => e.Price).IsRequired();
                entity.Property(e => e.IsNegotiable).IsRequired();
                entity.Property(e => e.Category).IsRequired();
                entity.Property(e => e.Title).IsRequired();
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
            modelBuilder.Entity<WishlistItem>(entity =>
            {
                entity.ToTable("WishlistItems");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnType("uuid")
                    .HasDefaultValueSql(UuidGenerationFunction)
                    .ValueGeneratedOnAdd();
                entity.HasOne(e => e.Product)
                    .WithMany()
                    .HasForeignKey(e => e.Product_Id)
                    .IsRequired();
                entity.Property(e => e.List_Id).IsRequired();
                // If Wishlist is another entity you should establish the relationship here
            });
            modelBuilder.Entity<ShoppingCartItem>(entity =>
            {
                entity.ToTable("ShoppingCartItems");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnType("uuid")
                    .HasDefaultValueSql(UuidGenerationFunction)
                    .ValueGeneratedOnAdd();
                entity.Property(e => e.Cart_Id)
                    .IsRequired();
                entity.HasOne(e => e.Product)
                    .WithMany()
                    .HasForeignKey(e => e.Product_Id)
                    .IsRequired();
                entity.Property(e => e.Quantity)
                    .IsRequired();
             });

            
        }
    }
}
