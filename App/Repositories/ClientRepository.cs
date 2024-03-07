using App.Presentations.Models;
using App.Utils;
using SQLite;

namespace App.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private SQLiteAsyncConnection database;

        public ClientRepository()
        {
            database = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DefaultValues.DATABASE_FILENAME));
            database.CreateTableAsync<Client>();
        }

        public async Task Add(Client client)
        {
            await database.InsertAsync(client);
        }

        public async Task Delete(int clientId)
        {
            var client = await Get(clientId);

            if (client != null && client.Id == clientId)
            {
                await database.DeleteAsync(client);
            }
        }

        public async Task<Client> Get(int clientId)
        {
            return await database.Table<Client>().Where(x => x.Id == clientId).FirstOrDefaultAsync();
        }

        public async Task<List<Client>> GetAll()
        {
            try
            {
                return await database.Table<Client>().ToListAsync();
            }
            catch (Exception)
            {
                return new List<Client>();
            }
        }

        public async Task Update(Client client)
        {
            await database.UpdateAsync(client);
        }
    }
}
