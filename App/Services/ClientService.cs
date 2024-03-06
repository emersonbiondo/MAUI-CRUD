using App.Presentations.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services
{
    public class ClientService
    {
        private static ClientService instance;

        private static readonly object lockService = new object();

        private ClientService() { }

        public static ClientService Instance()
        {
            lock(lockService)
            {
                if (instance == null)
                {
                    instance = new ClientService();
                    instance.Seed();
                }
            }
            return instance;
        }

        public List<Client> Clients { get; set; }

        private void Seed()
        {
            Clients = new List<Client>()
            {
                new Client()
                {
                    Id = 1,
                    Name = "Name 1",
                    LastName = "LastName 1",
                    Age = 20,
                    Address = "Address 1"
                },
                new Client()
                {
                    Id = 2,
                    Name = "Name 2",
                    LastName = "LastName 2",
                    Age = 30,
                    Address = "Address 2"
                }
            };
        }
    }
}
