using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RodosApi.Data;
using RodosApi.Domain;
using RodosApi.Domain.SortingDomain;

namespace RodosApi.Services
{
    public class MakerService :IMakerService
    {
        private readonly ApplicationDbContext _dbContext;

        public MakerService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Maker>> GetAllMakers(PaginationFilter filter = null, MakerSorting makerSorting = null, MakerFilter makerFilter = null)
        {
            var queryable = _dbContext.Makers.AsQueryable();
            if (filter is null)
            {
                return await queryable
                    .Include(s => s.Country)
                    .ToListAsync();
            }

            queryable = GetFiltered(queryable, makerFilter);
            queryable = GetSorted(queryable, makerSorting);

            var skip = (filter.PageNumber - 1) * filter.PageSize;
            return await queryable
                .Include(s => s.Country)
                .Skip(skip)
                .Take(filter.PageSize)
                .ToListAsync();

        }

       

        public async Task<Maker> GetMaker(long id)
        {
            var maker = await _dbContext.Makers.FirstOrDefaultAsync(s => s.MakerId == id);
            var country = await _dbContext.Countries.FirstOrDefaultAsync(s => s.CountryId == maker.CountryId);
            maker.Country = country;
            return maker;
        }

        public async Task<bool> CreateMaker(Maker makerToCreate)
        {
           makerToCreate.Country = await _dbContext.Countries.FirstOrDefaultAsync(s => s.CountryId == makerToCreate.CountryId);
            await _dbContext.Makers.AddAsync(makerToCreate);
            return await _dbContext.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<bool> UpdateMaker(Maker makerToUpdate)
        {
            makerToUpdate.Country = await _dbContext.Countries.FirstOrDefaultAsync(s => s.CountryId == makerToUpdate.CountryId);
            _dbContext.Makers.Update(makerToUpdate);
            return await _dbContext.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<bool> DeleteMaker(Maker makerToDelete)
        {
            _dbContext.Makers.Remove(makerToDelete);
            return await _dbContext.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<bool> ValidationMaker(string name, long? id = null)
        {
            var countryValidation = await _dbContext.Makers.Where(s =>
                s.Name.Trim().ToLower() == name.ToLower().Trim()
                && s.MakerId != id).FirstOrDefaultAsync();
            if (countryValidation != null)
            {
                return false;
            }
            return true;
        }

        private IQueryable<Maker> GetSorted(IQueryable<Maker> queryable, MakerSorting makerSorting)
        {
            switch (makerSorting.IdSort)
            {
                case 0:
                    break;
                case 1:
                    queryable = queryable.OrderBy(s => s.MakerId);
                    break;
                case 2:
                    queryable = queryable.OrderByDescending(s => s.MakerId);
                    break;
            }

            switch (makerSorting.NameSort)
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
            switch (makerSorting.CountryNameSort)
            {
                case 0:
                    break;
                case 1:
                    queryable = queryable.OrderBy(s => s.Country.Name);
                    break;
                case 2:
                    queryable = queryable.OrderByDescending(s => s.Country.Name);
                    break;
            }
            return queryable;

        }

        private IQueryable<Maker> GetFiltered(IQueryable<Maker> queryable, MakerFilter makerFilter)
        {
            if(makerFilter.CountryId != null)
            {
                queryable = queryable.Where(s => s.CountryId == makerFilter.CountryId);
            }

            if (makerFilter.Name != null)
            {
                queryable = queryable.Where(s => s.Name.Contains(makerFilter.Name));
            }
            return queryable;
        }
    }
}