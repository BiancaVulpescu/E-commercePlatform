using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
	public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
	{
		private const string UuidGenerationFunction = "uuid_generate_v4()";

		public DbSet<Product> Products { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<ShoppingCart> ShoppingCarts { get; set; }
		public DbSet<Wishlist> Wishlists { get; set; }
		public DbSet<Order> Orders { get; set; }

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
				entity.HasOne(e => e.Category)
					.WithMany()
					.HasForeignKey(e => e.CategoryId)
					.OnDelete(DeleteBehavior.SetNull);
			});
			modelBuilder.Entity<Category>(entity =>
			{
				entity.HasKey(e => e.Id);
				entity.Property(e => e.Id)
					.HasColumnType("uuid")
					.HasDefaultValueSql(UuidGenerationFunction)
					.ValueGeneratedOnAdd();
				entity.HasOne(e => e.ParentCategory)
					.WithMany(e => e.Subcategories)
					.HasForeignKey(e => e.ParentCategoryId)
					.OnDelete(DeleteBehavior.SetNull);
			});
			modelBuilder.Entity<Wishlist>(entity =>
			{
				entity.HasKey(e => e.Id);
				entity.Property(e => e.Id)
					.HasColumnType("uuid")
					.ValueGeneratedNever();
				entity.HasMany(e => e.Products)
					.WithMany(e => e.Wishlists);
			});
			modelBuilder.Entity<ShoppingCart>(entity =>
			{
				entity.HasKey(e => e.Id);
				entity.Property(e => e.Id)
					.HasColumnType("uuid")
					.ValueGeneratedNever();
				entity.HasMany(e => e.Products)
					.WithMany(e => e.ShoppingCarts)
					.UsingEntity<ShoppingCartProduct>(j => j.Property(e => e.Quantity));
			});
			modelBuilder.Entity<Order>(entity =>
			{
				entity.HasKey(e => e.Id);
				entity.Property(e => e.Id)
					.HasColumnType("uuid")
					.HasDefaultValueSql(UuidGenerationFunction)
					.ValueGeneratedOnAdd();
				entity.Property(e => e.UserId)
					.HasColumnType("uuid")
					.ValueGeneratedNever();

                entity.Property(e => e.City).IsRequired().HasMaxLength(55);
				entity.Property(e => e.Address).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Status).IsRequired().HasMaxLength(25);

				entity.HasMany(e => e.Products)
					.WithMany(e => e.Orders)
					.UsingEntity<OrderProduct>(j => j.Property(e => e.Quantity));
			});
		}
	}
}
