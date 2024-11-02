using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext context;
        public ProductRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<Guid> AddAsync(Product product)
        {
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();
            return product.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            var product = await GetByIdAsync(id);
            if(product != null)
            {
                context.Products.Remove(product);
                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            return await context.Products.FindAsync(id);
        }

        public async Task UpdateAsync(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
