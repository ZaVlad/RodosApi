using Microsoft.EntityFrameworkCore;
using RodosApi.Contract.V1.Request;
using RodosApi.Data;
using RodosApi.Domain;
using RodosApi.Domain.SortingDomain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using RodosApi.Dtos;
using RodosApi.Contract.V1.Request.Interfaces;

namespace RodosApi.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _dbContext;

        public OrderService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Order>> GetOrders(PaginationFilter pagination = null, OrderFilter orderFilter = null, OrderSorting orderSorting = null)
        {
            var queryable = _dbContext.Orders.AsQueryable();

            if (pagination is null)
            {
                return await queryable.Include(s => s.DeliveryStatus)
                .Include(s => s.Doors).ThenInclude(s => s.Door)
                .Include(s => s.DoorHandles).ThenInclude(s => s.DoorHandle)
                .Include(s => s.Hinges).ToListAsync();
            }

            queryable = GetFiltered(queryable, orderFilter);
            queryable = GetSorted(queryable, orderSorting);

            var skip = (pagination.PageNumber - 1) * pagination.PageSize;
            return await queryable.Include(s => s.Client)
                .Include(s => s.DeliveryStatus)
                .Include(s => s.Doors).ThenInclude(s => s.Door)
                .Include(s => s.DoorHandles).ThenInclude(s=>s.DoorHandle)
                .Include(s => s.Hinges).ThenInclude(s=>s.Hinges)
                .Skip(skip).Take(pagination.PageSize).ToListAsync();
        }


        public async Task<Order> GetOrder(long orderId)
        {
            var order = await _dbContext.Orders.Include(s => s.Client)
                .Include(s => s.DeliveryStatus)
                .Include(s => s.Doors).ThenInclude(s=>s.Door)
                .Include(s => s.DoorHandles).ThenInclude(s=>s.DoorHandle)
                .Include(s => s.Hinges).ThenInclude(s=>s.Hinges)
                .FirstOrDefaultAsync(s => s.OrderId == orderId);

            return order;
        }

        public async Task<bool> UpdateOrder(Order orderToUpdate, List<HingesForOrder> hingesForOrders, List<DoorHandleForOrder> doorHandleForOrders,List<DoorForOrder> doorForOrders)
        {
            orderToUpdate.Price = await GetGeneralPrice(orderToUpdate, hingesForOrders,doorHandleForOrders, doorForOrders);
            _dbContext.Entry(orderToUpdate).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            

            await UpdateForOrder(hingesForOrders, doorHandleForOrders, doorForOrders, orderToUpdate.OrderId);



            return await _dbContext.SaveChangesAsync() > 0 ? true : false;
        }

       

        public async Task<bool> CreateOrder(Order orderToCreate,List<HingesForOrder> hingesForOrders,List<DoorHandleForOrder> doorHandleForOrders, List<DoorForOrder> doorForOrders)
        {
            orderToCreate.Price = await GetGeneralPrice(orderToCreate, hingesForOrders, doorHandleForOrders, doorForOrders);
            await _dbContext.Orders.AddAsync(orderToCreate);
            await _dbContext.SaveChangesAsync();

            await CreateForOrder(doorHandleForOrders, hingesForOrders, orderToCreate.OrderId);

            return await _dbContext.SaveChangesAsync() > 0 ? true : false;
        }

       

        public async Task<bool> DeleteOrder(Order orderToDelete)
        {
            DeleteForOrder(orderToDelete.OrderId);
            _dbContext.Orders.Remove(orderToDelete);
            
            return await _dbContext.SaveChangesAsync() > 0 ? true : false;
        }


        public IOrderOperations OrderValidation(IOrderOperations order, out bool result)
        {
            if (order.DoorHandles != null)
            {
                foreach(var doorHandle in order.DoorHandles)
                {
                    if(doorHandle.DoorHandleId == 0)
                    {
                        doorHandle.DoorHandleQuantity = 0;
                    }
                    else
                    {
                        doorHandle.DoorHandleQuantity =doorHandle.DoorHandleQuantity == default ? 1 : doorHandle.DoorHandleQuantity;
                    }
                }
            }

            if(order.Doors != null)
            {

                foreach(var door in order.Doors)
                {
                    if(door.DoorId == 0)
                    {
                        door.DoorQunatity = 0;
                    }
                    else
                    {
                        door.DoorQunatity = door.DoorQunatity == default ? 1 : door.DoorQunatity;
                    }
                }

            }
            if (order.Hinges != null)
            {
                foreach (var hinge in order.Hinges)
                {
                    if (hinge.HingesId == 0)
                    {
                        hinge.HingesQuantity = 0;
                    }
                    else
                    {
                        hinge.HingesQuantity = hinge.HingesQuantity == default ? 1 : hinge.HingesQuantity;
                    }
                }
            }


            if (order.DoorHandles is null && order.Doors is null && order.Hinges is null)
            {
                result = false;
            }
            else
            {
                result = true;
            }

            return order;
        }

       

        private async Task CreateForOrder(List<DoorHandleForOrder> doorHandleForOrders, List<HingesForOrder> hingesForOrders, long orderId)
        {
            foreach(var doorHandle in doorHandleForOrders)
            {
                await _dbContext.OrderDoorHandles.AddAsync(new OrderDoorHandle()
                { 
                DoorHandleId = doorHandle.DoorHandleId, 
                DoorHandleQuantity = doorHandle.DoorHandleQuantity,
                OrderId = orderId
                });
            }
            foreach (var hinge in hingesForOrders)
            {
                await _dbContext.OrderHinges.AddAsync(new OrderHinges() { HingesId = hinge.HingesId, OrderId = orderId, HingesQuantity = hinge.HingesQuantity });
            }
        }
        private  async Task UpdateForOrder(List<HingesForOrder> hingesForOrders, List<DoorHandleForOrder> doorHandlesForOrders, List<DoorForOrder>doorsForOrders, long orderId)
        {
            var orderHingesToRemove =await  _dbContext.OrderHinges.Where(s => s.OrderId == orderId).ToListAsync();
            var orderDoorHandlesToRemove = await _dbContext.OrderDoorHandles.Where(s => s.OrderId == orderId).ToListAsync();
            var orderDoorToRemove = await _dbContext.OrderDoors.Where(s => s.OrderId == orderId).ToListAsync();
            var orderHingesToCreate = new List<OrderHinges>();
            var orderDoorHandlesToCreate = new List<OrderDoorHandle>();
            var orderDoorToCreate = new List<OrderDoor>();

            foreach (var item in orderHingesToRemove)
            {
                _dbContext.Entry(item).State = EntityState.Deleted;
            }

            foreach(var item in orderDoorHandlesToRemove)
            {
                _dbContext.Entry(item).State = EntityState.Deleted;
            }

            foreach(var item in orderDoorToRemove)
            {
                _dbContext.Entry(item).State = EntityState.Deleted;
            }

            foreach (var hinge in hingesForOrders)
            {
                orderHingesToCreate.Add(new OrderHinges() { HingesId = hinge.HingesId, OrderId = orderId, HingesQuantity = hinge.HingesQuantity });
            }

            foreach(var doorHandle in doorHandlesForOrders)
            {
                orderDoorHandlesToCreate.Add(new OrderDoorHandle() { DoorHandleId = doorHandle.DoorHandleId, OrderId = orderId, DoorHandleQuantity = doorHandle.DoorHandleQuantity });
            }

            foreach(var door in doorsForOrders) 
            {
                orderDoorToCreate.Add(new OrderDoor() { DoorId = door.DoorId, OrderId = orderId, DoorQuantity = door.DoorQunatity });
            }
            await _dbContext.OrderHinges.AddRangeAsync(orderHingesToCreate);
            await _dbContext.OrderDoorHandles.AddRangeAsync(orderDoorHandlesToCreate);
            await _dbContext.OrderDoors.AddRangeAsync(orderDoorToCreate);
        }
        private void DeleteForOrder(long orderId)
        {
            var orderHingesToDelete = _dbContext.OrderHinges.Where(s => s.OrderId == orderId);
            var orderDoorHandlesToDelete = _dbContext.OrderDoorHandles.Where(s => s.OrderId == orderId);
            var orderDoorToDelete = _dbContext.OrderDoors.Where(s => s.OrderId == orderId);
            _dbContext.OrderDoorHandles.RemoveRange(orderDoorHandlesToDelete);
            _dbContext.OrderHinges.RemoveRange(orderHingesToDelete);
            _dbContext.OrderDoors.RemoveRange(orderDoorToDelete);
        }


        private async Task<decimal> GetGeneralPrice(Order order,List<HingesForOrder> hingesForOrders,List<DoorHandleForOrder> doorHandleForOrders,List<DoorForOrder> doorForOrders)
        {
            decimal price =0;
            
            foreach(var hinge in hingesForOrders)
            {
                var hingeFromdb =await _dbContext.Hinges.FirstOrDefaultAsync(s => s.HingesId == hinge.HingesId);
                price += hingeFromdb.Price * hinge.HingesQuantity;
            }

            foreach(var doorhandle in doorHandleForOrders)
            {
                var doorHandleFromDb = await _dbContext.DoorHandles.FirstOrDefaultAsync(s => s.DoorHandleId == doorhandle.DoorHandleId);
                price += doorHandleFromDb.Price * doorhandle.DoorHandleQuantity;
            }

          foreach(var door in doorForOrders)
            {
                var doorFromDb = await _dbContext.Doors.FirstOrDefaultAsync(s => s.DoorId == door.DoorId);
                price += doorFromDb.Price * door.DoorQunatity;
            }
            return price;

        }
        private IQueryable<Order> GetFiltered(IQueryable<Order> queryable, OrderFilter orderFilter)
        {
            if (orderFilter.ClientFilterId != null)
            {
                queryable = queryable.Where(s => s.ClientId == orderFilter.ClientFilterId);
            }

            if (orderFilter.DeliveryStatusFilterId != null)
            {
                queryable = queryable.Where(s => s.DeliveryStatusId == orderFilter.DeliveryStatusFilterId);
            }


            return queryable;
        }

        private IQueryable<Order> GetSorted(IQueryable<Order> queryable, OrderSorting orderSorting)
        {
            switch (orderSorting.ClientNameSorting)
            {
                case 0:
                    break;
                case 1:
                    queryable = queryable.OrderBy(s => s.Client.Name);
                    break;
                case 2:
                    queryable = queryable.OrderByDescending(s => s.Client.Name);
                    break;
            }

            switch (orderSorting.PriceSorting)
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

            switch (orderSorting.OrderDateSorting)
            {
                case 0:
                    break;
                case 1:
                    queryable = queryable.OrderBy(s => s.OrderDate);
                    break;
                case 2:
                    queryable = queryable.OrderByDescending(s => s.OrderDate);
                    break;
            }

            switch (orderSorting.OrderIdSorting)
            {
                case 0:
                    break;
                case 1:
                    queryable = queryable.OrderBy(s => s.OrderId);
                    break;
                case 2:
                    queryable = queryable.OrderByDescending(s => s.OrderId);
                    break;
            }
            return queryable;
        }


    }
}
