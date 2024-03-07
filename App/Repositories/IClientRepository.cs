using App.Presentations.Models;

namespace App.Repositories
{
    public interface IClientRepository
    {
        Task<List<Client>> GetAll();
        Task<Client> Get(int clientId);
        Task Add(Client client);
        Task Update(Client client);
        Task Delete(int clientId);
    }
}
