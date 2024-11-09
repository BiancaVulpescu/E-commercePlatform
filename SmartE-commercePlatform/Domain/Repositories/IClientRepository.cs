using Domain.Entities;

namespace Domain.Repositories
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetAllAsync();
        Task<Client> GetByIdAsync(Guid id);
        Task<Guid> AddAsync(Client client);
        Task UpdateAsync(Client client);
        Task DeleteAsync(Guid id);
    }
}
