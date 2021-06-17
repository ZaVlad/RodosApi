using RodosApi.Domain;
using RodosApi.Domain.SortingDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RodosApi.Services
{
    public interface IClientService
    {
        public Task<List<Client>> GetClients(PaginationFilter pagination = null, string name = null, ClientSorting clientSorting = null);
        public Task<Client> GetClient(long clientId);
        public Task<bool> CreateClient(Client client);
        public Task<bool> UpdateClient(Client client);
        public Task<bool> DeleteClient(Client client);
        public Task<bool> ClientValidation(string email, long? Id = null);
    }
}
