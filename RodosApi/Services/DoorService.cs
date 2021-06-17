using Microsoft.EntityFrameworkCore;
using RodosApi.Contract.V1.Response;
using RodosApi.Data;
using RodosApi.Domain;
using RodosApi.Domain.SortingDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RodosApi.Services
{
    public class DoorService : IDoorService
    {
        private readonly ApplicationDbContext _dbContext;

        public DoorService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CreateDoor(Door doorToCreate)
        {
            await _dbContext.Doors.AddAsync(doorToCreate);
            //_dbContext.Entry(doorToCreate).State = EntityState.Unchanged;

            return await _dbContext.SaveChangesAsync() > 0 ? true : false;
        }
        public async Task<bool> CreateDoors(List<Door> doorsToCreate)
        {
            bool itsWork;
            foreach (var door in doorsToCreate)
            {
                itsWork = await CreateDoor(door);
                if (itsWork == false)
                {
                    return false;
                }
            }
            return true;
        }

        public async Task<bool> DeleteDoor(Door doorToDelete)
        {
            _dbContext.Doors.Remove(doorToDelete);
            return await _dbContext.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<Door> GetDoor(long id)
        {
            return await _dbContext.Doors.Include(s => s.Category)
                    .Include(s => s.Coating)
                    .Include(s => s.Collection)
                    .Include(s => s.Color)
                    .Include(s => s.DoorHandle)
                    .Include(s => s.DoorModel)
                    .Include(s => s.Hinges)
                    .Include(s => s.Maker)
                    .Include(s => s.TypeOfDoor)
                    .FirstOrDefaultAsync(s => s.DoorId == id);
        }
        public async Task<List<Door>> GetDoors(DoorFilter doorFilter = null, PaginationFilter filter = null, DoorSorting doorSorting = null)
        {
            var queryable = _dbContext.Doors.AsQueryable();
            List<Door> doors;
            if (filter == null)
            {
                doors = await queryable
                    .Include(s => s.Category)
                    .Include(s => s.Coating)
                    .Include(s => s.Collection)
                    .Include(s => s.Color)
                    .Include(s => s.DoorHandle)
                    .Include(s => s.DoorModel)
                    .Include(s => s.Hinges)
                    .Include(s => s.Maker)
                    .Include(s => s.TypeOfDoor)
                    .ToListAsync();
                doors = await GetAllObjects(doors);
                return doors;
            }
            queryable = GetFiltered(queryable, doorFilter);
            queryable = GetSorted(queryable, doorSorting);

            var skip = (filter.PageNumber - 1) * filter.PageSize;
            doors =  await queryable
                    .Include(s => s.Category)
                    .Include(s => s.Coating)
                    .Include(s => s.Collection)
                    .Include(s => s.Color)
                    .Include(s => s.DoorHandle)
                    .Include(s => s.DoorModel)
                    .Include(s => s.Hinges)
                    .Include(s => s.Maker)
                    .Include(s => s.TypeOfDoor)
                    .Skip(skip)
                    .Take(filter.PageSize).ToListAsync();
            doors = await GetAllObjects(doors);

            return doors;
        }

        

        public async Task<bool> UpdateDoor(Door doorToUpdate)
        {
            _dbContext.Doors.Update(doorToUpdate);
            return await _dbContext.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<bool> ValidationDoor(string name, long? id = null)
        {
            var validation = await
                _dbContext.Doors.Where(s => s.Name.Trim().ToLower() == name.Trim().ToLower()
                                             && s.DoorId != id).FirstOrDefaultAsync();
            if (validation != null)
            {
                return false;
            }

            return true;
        }

        private IQueryable<Door> GetFiltered(IQueryable<Door> queryable,DoorFilter filter)
        {
            if (filter.CategoryId != null)
            {
                queryable = queryable.Where(s => s.CategoryId == filter.CategoryId);
            }

            if (filter.CoatingId != null)
            {
                queryable = queryable.Where(s => s.CoatingId == filter.CoatingId);
            }

            if (filter.CollectionId != null)
            {
                queryable = queryable.Where(s => s.CollectionId == filter.CollectionId);
            }

            if (filter.ColorId != null)
            {
                queryable = queryable.Where(s => s.ColorId == filter.ColorId);
            }

            if (filter.DoorHandleId != null)
            {
                queryable = queryable.Where(s => s.DoorHandleId == filter.DoorHandleId);
            }

            if (filter.HingesId != null)
            {
                queryable = queryable.Where(s => s.HingesId == filter.HingesId);
            }

            if (filter.MakerId != null)
            {
                queryable = queryable.Where(s => s.MakerId == filter.MakerId);
            }
            if (filter.Name != null)
            {
                queryable = queryable.Where(s => s.Name.Contains(filter.Name));
            }
            if (filter.TypeOfDoorId != null)
            {
                queryable = queryable.Where(s => s.TypeOfDoorId == filter.TypeOfDoorId);
            }
            return queryable;
        }

        private IQueryable<Door> GetSorted(IQueryable<Door> queryable, DoorSorting doorSorting)
        {
            switch (doorSorting.IdSort)
            {
                case 0:
                    break;
                case 1:
                    queryable = queryable.OrderBy(s => s.DoorId);
                    break;
                case 2:
                    queryable = queryable.OrderByDescending(s => s.DoorId);
                    break;
            }

            switch (doorSorting.CoatingNameSort)
            {
                case 0:
                    break;
                case 1:
                    queryable = queryable.OrderBy(s => s.Coating.Name);
                    break;
                case 2:
                    queryable = queryable.OrderByDescending(s => s.Coating.Name);
                    break;
            }

            switch (doorSorting.CollectionNameSort)
            {
                case 0:
                    break;
                case 1:
                    queryable = queryable.OrderBy(s => s.Collection.Name);
                    break;
                case 2:
                    queryable = queryable.OrderByDescending(s => s.Collection.Name);
                    break;
            }

            switch (doorSorting.ColorNameSort)
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

            switch (doorSorting.DoorHandleNameSort)
            {
                case 0:
                    break;
                case 1:
                    queryable = queryable.OrderBy(s => s.DoorHandle.Name);
                    break;
                case 2:
                    queryable = queryable.OrderByDescending(s => s.DoorHandle.Name);
                    break;
            }

            switch (doorSorting.DoorModelNameSort)
            {
                case 0:
                    break;
                case 1:
                    queryable = queryable.OrderBy(s => s.DoorModel.Name);
                    break;
                case 2:
                    queryable = queryable.OrderByDescending(s => s.DoorModel.Name);
                    break;
            }

            switch (doorSorting.HingesNameSort)
            {
                case 0:
                    break;
                case 1:
                    queryable = queryable.OrderBy(s => s.Hinges.Name);
                    break;
                case 2:
                    queryable = queryable.OrderByDescending(s => s.Hinges.Name);
                    break;
            }

            switch (doorSorting.MakerNameSort)
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

            switch (doorSorting.NameSort)
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

            switch (doorSorting.PriceSort)
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

        private async  Task<List<Door>> GetAllObjects(List<Door> doors)
        {
            foreach(var door in doors)
            {
                door.Category = await _dbContext.Categories.FindAsync(door.CategoryId);
                door.Coating = await _dbContext.Coatings.FindAsync(door.CoatingId);
                door.Collection = await _dbContext.Collections.FindAsync(door.CollectionId);
                door.Color = await _dbContext.Colors.FindAsync(door.ColorId);
                door.DoorHandle = await _dbContext.DoorHandles.FindAsync(door.DoorHandleId);
                door.DoorModel = await _dbContext.DoorModels.FindAsync(door.DoorModelId);
                door.Hinges = await _dbContext.Hinges.FindAsync(door.HingesId);
                door.Maker = await _dbContext.Makers.FindAsync(door.MakerId);
                door.TypeOfDoor = await _dbContext.TypesOfDoors.FindAsync(door.TypeOfDoorId);
            }
            return doors;
        }

    }
}
