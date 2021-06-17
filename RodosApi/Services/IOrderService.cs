using RodosApi.Contract.V1.Request;
using RodosApi.Contract.V1.Request.Interfaces;
using RodosApi.Domain;
using RodosApi.Domain.SortingDomain;
using RodosApi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RodosApi.Services
{
    public interface IOrderService
    {
        public Task<List<Order>> GetOrders(PaginationFilter pagination = null, OrderFilter orderFilter = null, OrderSorting orderSorting = null);
        public Task<Order> GetOrder(long orderId);
        public Task<bool> CreateOrder(Order orderToCreate, List<HingesForOrder> hingesForOrders, List<DoorHandleForOrder> doorHandleForOrders, List<DoorForOrder> doorForOrders);
        public Task<bool> UpdateOrder(Order orderToUpdate, List<HingesForOrder> hingesForOrders, List<DoorHandleForOrder> doorHandleForOrders, List<DoorForOrder> doorForOrders);
        public Task<bool> DeleteOrder(Order orderToDelete);
        public IOrderOperations OrderValidation(IOrderOperations order,out bool result);
    }
}
