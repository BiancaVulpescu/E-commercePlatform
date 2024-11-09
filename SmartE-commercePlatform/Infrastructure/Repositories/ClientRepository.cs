using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly ApplicationDbContext context;
        public ClientRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Guid> AddAsync(Client client)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Client> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Client client)
        {
            throw new NotImplementedException();
        }
    }
}
