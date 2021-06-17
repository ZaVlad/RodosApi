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
    public class HingesService : IHingesService
    {
        private readonly ApplicationDbContext _dbContext;

        public HingesService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CreateHinges(Hinges hingeToCreate)
        {
            await _dbContext.AddAsync(hingeToCreate);
            return await _dbContext.SaveChangesAsync() > 0 ? true : false;
        }

        public async  Task<bool> DeleteHinges(Hinges hingeToDelete)
        {
            _dbContext.Hinges.Remove(hingeToDelete);
            return await _dbContext.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<List<Hinges>> GetHinges(GetAllHingesFilter hingesFilter = null, PaginationFilter filter = null, HingesSorting hingesSorting = null)
        {
            var quearyable = _dbContext.Hinges.AsQueryable();
            if(filter == null)
            {
                return await quearyable
                    .Include(s=>s.Category)
                    .Include(s=>s.FurnitureType)
                    .Include(s=>s.Maker)
                    .Include(c=>c.Material)
                    .Include(g=>g.TypeOfHinge)
                    .ToListAsync();
            }
            quearyable = GetFiltered(quearyable,hingesFilter );
            quearyable = GetSorted(quearyable, hingesSorting);

            var skip = (filter.PageNumber - 1) * filter.PageSize;
            return await quearyable.Include(s => s.Category)
                    .Include(s => s.FurnitureType)
                    .Include(s => s.Maker)
                    .Include(c => c.Material)
                    .Include(g => g.TypeOfHinge)
                    .Skip(skip)
                    .Take(filter.PageSize)
                    .ToListAsync();


        }

    

        public async Task<Hinges> GetHinge(long id)
        {
            var hinge = await _dbContext.Hinges.Include(s => s.Category)
                    .Include(s => s.FurnitureType)
                    .Include(s => s.Maker)
                    .Include(c => c.Material)
                    .Include(g => g.TypeOfHinge)
                    .FirstOrDefaultAsync(s => s.HingesId == id);
            return hinge;
        }

        public async Task<bool> UpdateHinges(Hinges hingeToUpdate)
        {
            _dbContext.Hinges.Update(hingeToUpdate);
            return await _dbContext.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<bool> ValidationHinges(string name, long? id = null)
        {
            var hinge = await _dbContext.Hinges.Where(s => s.Name.Trim().ToLower() == name.Trim().ToLower()
                                                                  && s.HingesId != id).FirstOrDefaultAsync();
            if (hinge != null)
            {
                return false;
            }

            return true;
        }
        private static IQueryable<Hinges> GetFiltered(IQueryable<Hinges> quaryable, GetAllHingesFilter filter)
        {
            if(filter.CategoryId != null) 
            {
                quaryable = quaryable.Where(s => s.CategoryId == filter.CategoryId);
            }

            if(filter.FurnitureTypeId != null)
            {
                quaryable = quaryable.Where(s => s.FurnitureTypeId == filter.FurnitureTypeId);
            }

            if(filter.MaterialId != null)
            {
                quaryable = quaryable.Where(s => s.MaterialId == filter.MaterialId);
            }
            if(filter.Name != null)
            {
                quaryable = quaryable.Where(s => s.Name.Contains(filter.Name));
            }
            if(filter.TypeOFHingesId != null)
            {
                quaryable = quaryable.Where(s => s.TypeOfHingesId == filter.TypeOFHingesId);
            }
            if(filter.MakerId != null)
            {
                quaryable = quaryable.Where(s => s.MakerId == filter.MaterialId);
            }
            return quaryable;
        }

        private IQueryable<Hinges> GetSorted(IQueryable<Hinges> quearyable, HingesSorting hingesSorting)
        {
            switch (hingesSorting.IdSort)
            {
                case 0:
                    break;
                case 1:
                    quearyable = quearyable.OrderBy(s => s.HingesId);
                    break;
                case 2:
                    quearyable = quearyable.OrderByDescending(s => s.HingesId);
                    break;
            }


            switch (hingesSorting.MakerNameSort)
            {
                case 0:
                    break;
                case 1:
                    quearyable = quearyable.OrderBy(s => s.Maker.Name);
                    break;
                case 2:
                    quearyable = quearyable.OrderByDescending(s => s.Maker.Name);
                    break;
            }


            switch (hingesSorting.MaterialNameSort)
            {
                case 0:
                    break;
                case 1:
                    quearyable = quearyable.OrderBy(s => s.Material.Name);
                    break;
                case 2:
                    quearyable = quearyable.OrderByDescending(s => s.Material.Name);
                    break;
            }

            switch (hingesSorting.NameSort)
            {
                case 0:
                    break;
                case 1:
                    quearyable = quearyable.OrderBy(s => s.Name);
                    break;
                case 2:
                    quearyable = quearyable.OrderByDescending(s => s.Name);
                    break;
            }

            switch (hingesSorting.TypeOfHingeNameSort)
            {
                case 0:
                    break;
                case 1:
                    quearyable = quearyable.OrderBy(s => s.TypeOfHinge.Name);
                    break;
                case 2:
                    quearyable = quearyable.OrderByDescending(s => s.TypeOfHinge.Name);
                    break;
            }


            switch (hingesSorting.PriceSort)
            {
                case 0:
                    break;
                case 1:
                    quearyable = quearyable.OrderBy(s => s.Price);
                    break;
                case 2:
                    quearyable = quearyable.OrderByDescending(s => s.Price);
                    break;
            }
            return quearyable;
        }
    }
}
