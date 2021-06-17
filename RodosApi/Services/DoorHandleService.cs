using Microsoft.EntityFrameworkCore;
using RodosApi.Data;
using RodosApi.Domain;
using RodosApi.Domain.SortingDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RodosApi.Services
{
    public class DoorHandleService : IDoorHandleService
    {
        private readonly ApplicationDbContext _dbContext;

        public DoorHandleService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CreateDoorHandle(DoorHandle doorHandleToCreate)
        {
            await _dbContext.DoorHandles.AddAsync(doorHandleToCreate);
            return await _dbContext.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<bool> DeletedoorHandle(DoorHandle doorHandleToDelete)
        {
            _dbContext.DoorHandles.Remove(doorHandleToDelete);
            return await _dbContext.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<List<DoorHandle>> GetAllDoorHandles(DoorHandleFilter doorHandlesFilter = null, PaginationFilter filter = null, DoorHandlesSorting doorHandlesSorting = null)
        {
            var queryable = _dbContext.DoorHandles.AsQueryable();
            if (filter == null)
            {
                return await queryable
                    .Include(s => s.Category)
                    .Include(s => s.Color)
                    .Include(c => c.FurnitureType)
                    .Include(f => f.Maker)
                    .ThenInclude(s=>s.Country)
                    .Include(c => c.Material)
                    .ToListAsync();
            }


           queryable =  GetFiltered(queryable, doorHandlesFilter);
           queryable =  GetSorted(queryable, doorHandlesSorting);

            var skip = (filter.PageNumber - 1) * filter.PageSize;
            return await queryable
                    .Include(s => s.Category)
                    .Include(s => s.Color)
                    .Include(c => c.FurnitureType)
                    .Include(f => f.Maker)
                    .Include(c => c.Material)
                    .Skip(skip)
                    .Take(filter.PageSize)
                    .ToListAsync();
        }

      

        public async Task<DoorHandle> GetDoorHandle(long id)
        {
            return await _dbContext.DoorHandles
                    .Include(s => s.Category)
                    .Include(s => s.Color)
                    .Include(c => c.FurnitureType)
                    .Include(f => f.Maker)
                    .Include(c => c.Material)
                    .FirstOrDefaultAsync(s => s.DoorHandleId == id);
        }

        public async Task<bool> UpdateDoorHandle(DoorHandle doorHandleToUpdate)
        {
            _dbContext.DoorHandles.Update(doorHandleToUpdate);
            return await _dbContext.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<bool> ValidationdoorHandle(string name, long? id = null)
        {
            var validation = await
               _dbContext.DoorHandles.Where(s => s.Name.Trim().ToLower() == name.Trim().ToLower()
                                            && s.DoorHandleId != id).FirstOrDefaultAsync();
            if (validation != null)
            {
                return false;
            }

            return true;
        }
        private static IQueryable<DoorHandle> GetFiltered(IQueryable<DoorHandle> queryable, DoorHandleFilter filter)
        {
            if (filter.CategoryId != null)
            {
                queryable = queryable.Where(s => s.CategoryId == filter.CategoryId);
            }
            if (filter.ColorId != null)
            {
                queryable = queryable.Where(s => s.ColorId == filter.ColorId);
            }
            if (filter.FurnitureTypeId != null)
            {
                queryable = queryable.Where(s => s.FurnitureTypeId == filter.FurnitureTypeId);
            }
            if (filter.MakerId != null)
            {
                queryable = queryable.Where(s => s.MakerId == filter.MakerId);
            }
            if (filter.MaterialId != null)
            {
                queryable = queryable.Where(c => c.MaterialId == filter.MaterialId);
            }
            if (filter.Name != null)
            {
                queryable = queryable.Where(s => s.Name.Contains(filter.Name));
            }
            return queryable;
        }

        private IQueryable<DoorHandle> GetSorted(IQueryable<DoorHandle> queryable, DoorHandlesSorting doorHandlesSorting)
        {
            switch (doorHandlesSorting.ColorNameSort)
            {
                case 0:
                    break;
                case 1:
                    queryable = queryable.OrderBy(s => s.Color.Name);
                    break;
                case 2:
                    queryable = queryable.OrderByDescending(s => s.Color.Name);
                    break;
            }

            switch (doorHandlesSorting.IdSort)
            {
                case 0:
                    break;
                case 1:
                    queryable = queryable.OrderBy(s => s.DoorHandleId);
                    break;
                case 2:
                    queryable = queryable.OrderByDescending(s => s.DoorHandleId);
                    break;
            }

            switch (doorHandlesSorting.MakerNameSort)
            {
                case 0:
                    break;
                case 1:
                    queryable = queryable.OrderBy(s => s.Maker.Name);
                    break;
                case 2:
                    queryable = queryable.OrderByDescending(s => s.Maker.Name);
                    break;
            }

            switch (doorHandlesSorting.MaterialNameSort)
            {
                case 0:
                    break;
                case 1:
                    queryable = queryable.OrderBy(s => s.Material.Name);
                    break;
                case 2:
                    queryable = queryable.OrderByDescending(s => s.Material.Name);
                    break;
            }

            switch (doorHandlesSorting.NameSort)
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


            switch (doorHandlesSorting.PriceSort)
            {
                case 0:
                    break;
                case 1:
                    queryable = queryable.OrderBy(s => s.Price);
                    break;
                case 2:
                    queryable = queryable.OrderByDescending(s => s.Price);
                    break;
            }
            return queryable;
        }
    }
}
