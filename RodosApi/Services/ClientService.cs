using RodosApi.Data;
using RodosApi.Domain;
using RodosApi.Domain.SortingDomain;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace RodosApi.Services
{
    public class ClientService : IClientService
    {
        private readonly ApplicationDbContext _dbContext;
        public ClientService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Client>> GetClients(PaginationFilter pagination = null, string name = null, ClientSorting clientSorting = null)
        {
            var queryable = _dbContext.Clients.AsQueryable();

            if(pagination == null)
            {
                return await queryable.ToListAsync();
            }

            queryable = GetFiltered(queryable, name);
            queryable = GetSorted(queryable, clientSorting);

            var skip = (pagination.PageNumber - 1) * pagination.PageSize;

            return await queryable.Skip(skip).Take(pagination.PageSize).ToListAsync();
        }

       

        public async Task<Client> GetClient(long clientId)
        {
            var client = await _dbContext.Clients.FindAsync(clientId);

            return client;
        }

        public async Task<bool> UpdateClient(Client client)
        {
            _dbContext.Clients.Update(client);
            return await _dbContext.SaveChangesAsync()>0?true:false;
        }

        public async Task<bool> CreateClient(Client client)
        {
            await  _dbContext.Clients.AddAsync(client);
            return await _dbContext.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<bool> DeleteClient(Client client)
        {
            _dbContext.Clients.Remove(client);
            return await _dbContext.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<bool> ClientValidation(string email, long? Id = null)
        {
            var client =await _dbContext.Clients.Where(s => s.Name.Trim().ToLower() == email.ToLower().Trim()
            && s.ClientId != Id).FirstOrDefaultAsync() ;
            if(client != null)
            {
                return false;
            }
            return true;
        }


        private IQueryable<Client> GetSorted(IQueryable<Client> queryable, ClientSorting clientSorting)
        {
            switch (clientSorting.AdressSort)
            {
                case 0: 
                    break;
                case 1:
                    queryable = queryable.OrderBy(s => s.Address);
                    break;
                case 2:
                    queryable = queryable.OrderByDescending(s => s.Address);

                    break;
            }

            switch (clientSorting.ClientIdSort)
            {
                case 0:
                    break;
                case 1:
                    queryable = queryable.OrderBy(s => s.ClientId);
                    break;
                case 2:
                    queryable = queryable.OrderByDescending(s => s.ClientId);

                    break;
            }

            switch (clientSorting.EmailSort)
            {
                case 0:
                    break;
                case 1:
                    queryable = queryable.OrderBy(s => s.Email);
                    break;
                case 2:
                    queryable = queryable.OrderByDescending(s => s.Email);

                    break;
            }

            switch (clientSorting.LastNameSort)
            {
                case 0:
                    break;
                case 1:
                    queryable = queryable.OrderBy(s => s.LastName);
                    break;
                case 2:
                    queryable = queryable.OrderByDescending(s => s.LastName);

                    break;
            }

            switch (clientSorting.NameSort)
            {
                case 0:
                    break;
                case 1:
                    queryable = queryable.OrderBy(s => s.Name);
                    break;
                case 2:
                    queryable = queryable.OrderByDescending(s => s.Name);

                    break;
            }
            return queryable;
        }

        private IQueryable<Client> GetFiltered(IQueryable<Client> queryable, string name)
        {
            if(name != null)
            {
                queryable = queryable.Where(s => s.Name.Contains(name));
            }
            return queryable;
        }


    }
}
