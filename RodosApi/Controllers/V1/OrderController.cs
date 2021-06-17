using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RodosApi.Contract;
using RodosApi.Contract.V1.Request;
using RodosApi.Contract.V1.Request.Queries;
using RodosApi.Contract.V1.Response;
using RodosApi.Domain;
using RodosApi.Domain.SortingDomain;
using RodosApi.Helpers;
using RodosApi.Services;
using System;
using RodosApi.HelperClassForResponse;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RodosApi.Controllers.V1
{
    public class OrderController :Controller
    {
        private readonly IUriService _uriService;
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IUriService uriService, IOrderService orderService, IMapper mapper)
        {
            _uriService = uriService;
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpGet(ApiRoutes.Order.GetOrders)]
        public async Task<IActionResult> GetOrders(PaginationQuery paginationQuery,OrderFilterQuery orderFilter,OrderSortingQuery orderSorting)
        {
            var pagination = _mapper.Map<PaginationFilter>(paginationQuery);
            var filter = _mapper.Map<OrderFilter>(orderFilter);
            var sorting = _mapper.Map<OrderSorting>(orderSorting);

            var orders = await _orderService.GetOrders(pagination, filter, sorting);
            var ordersResponse = MapOrdersForResponse(orders);

            if (pagination is null || pagination.PageNumber < 1 || pagination.PageSize < 1)
            {
                return Ok(ordersResponse);
            }

            var result = PaginationHelpers.CreatePaginationResponse(_uriService, pagination, ordersResponse);

            return Ok(result);
        }
        [HttpGet(ApiRoutes.Order.GetOrder)]
        public async Task<IActionResult> GetOrder(long orderId)
        {
            var order = await _orderService.GetOrder(orderId);
            if(order is null)
            {
                return NotFound();
            }
            var orderResponse = MapOrderForResponse(order);
            return Ok(orderResponse);
        }

        [HttpPost(ApiRoutes.Order.CreateOrder)]
        public async Task<IActionResult> CreateOrder([FromBody] OrderToCreate orderToCreate)
        {
            bool orderValidation; 
            orderToCreate = _orderService.OrderValidation(orderToCreate,out orderValidation) as OrderToCreate;

            if(orderValidation == false)
            {
                ModelState.AddModelError("", "You can't create order without any items for purchase");
                return BadRequest(ModelState);
            }

            var order = new Order() { ClientId = orderToCreate.ClientId };
            order.OrderDate = DateTime.UtcNow;
            order.DeliveryStatusId = 1;

            if(await _orderService.CreateOrder(order, orderToCreate.Hinges,orderToCreate.DoorHandles,orderToCreate.Doors) == false)
            {
                ModelState.AddModelError("", "Something went wrong");
                return BadRequest(ModelState);
            }

            var baseUri = string.Concat(_uriService.BaseUri(), ApiRoutes.Order.GetOrder).Replace("{orderId}", order.OrderId.ToString());

            return Created(new Uri(baseUri),$"Order with this Id {order.OrderId} was created at {order.OrderDate}");
        }




        [HttpPut(ApiRoutes.Order.UpdateOrder)]

        public async Task<IActionResult> UpdateOrder(long orderId,[FromBody]OrderToUpdate orderToUpdate)
        {
            bool orderValidation;
            orderToUpdate = _orderService.OrderValidation(orderToUpdate,out orderValidation) as OrderToUpdate;

            if(orderValidation == false)
            {
                ModelState.AddModelError("", "You can't update order without added any items for purchase");
                return BadRequest(ModelState);
            }

            var order = await _orderService.GetOrder(orderId);

            if(order is null)
            {
                return NotFound();
            }

            order = MapOrderForUpdate(order, orderToUpdate);

            if(await _orderService.UpdateOrder(order, orderToUpdate.Hinges,orderToUpdate.DoorHandles,orderToUpdate.Doors) == false)
            {
                ModelState.AddModelError("", "Something went wrong");
                return BadRequest(ModelState);
            }
            return Ok(MapOrderForResponse(order));
        }

        [HttpDelete(ApiRoutes.Order.DeleteOrder)]

        public async Task<IActionResult> DeleteOrder(long orderId)
        {
            var order = await _orderService.GetOrder(orderId);
            if(order is null)
            {
                return NotFound();
            }

            if(await _orderService.DeleteOrder(order) == false)
            {
                ModelState.AddModelError("", "Something went wrong");
                return BadRequest(ModelState);
            }
            return NoContent();
        }

        private Order MapOrderForUpdate(Order order, OrderToUpdate orderToUpdate)
        {
            order.ClientId = orderToUpdate.ClientId;
            order.DeliveryStatusId = (byte)orderToUpdate.DeliveryStatusId;
            order.OrderDate = orderToUpdate.OrderDate;
            return order;
        }
        private List<OrderResponse> MapOrdersForResponse(List<Order> orders)
        {
            var ordersResponse = new List<OrderResponse>();
            foreach(var order in orders)
            {
                ordersResponse.Add(new OrderResponse()
                {
                    OrderId = order.OrderId,
                    OrderDate = order.OrderDate,
                    ClientResponse = _mapper.Map<ClientResponse>(order.Client),
                    DeliveryStatus = order.DeliveryStatus,
                    Price = order.Price,
                    DoorHandle = GetDoorHandlesOrderResponse(order),
                    Doors = GetDoorsOrderResponse(order),
                    Hinges = GetHingesOrderResponse(order)
                }); 
            }
            return ordersResponse;
        }

        private OrderResponse MapOrderForResponse(Order order)
        {
            var orderForResponse = new OrderResponse()
            {
                OrderId = order.OrderId,
                OrderDate = order.OrderDate,
                ClientResponse = _mapper.Map<ClientResponse>(order.Client),
                DeliveryStatus = order.DeliveryStatus,
                Price = order.Price,
                Doors = GetDoorsOrderResponse(order),
                DoorHandle = GetDoorHandlesOrderResponse(order),
                Hinges = GetHingesOrderResponse(order)
            };
            return orderForResponse;
        }
        private List<HingesOrderResponse> GetHingesOrderResponse(Order order)
        {
            var hingesOrderResponseArray = new List<HingesOrderResponse>();
            foreach (var hinge in order.Hinges)
            {
                hingesOrderResponseArray.Add(new HingesOrderResponse()
                {
                    Hinges = _mapper.Map<HingesResponse>(hinge.Hinges),
                    HingesQuantity = hinge.HingesQuantity
                });
            }
            return hingesOrderResponseArray;
        }
        private List<DoorHandleOrderResponse> GetDoorHandlesOrderResponse(Order order)
        {
            var doorHandleOrderResponseArry = new List<DoorHandleOrderResponse>();
            foreach(var doorHandle in order.DoorHandles)
            {
                doorHandleOrderResponseArry.Add(new DoorHandleOrderResponse()
                {
                    DoorHandle = _mapper.Map<DoorHandleResponse>(doorHandle.DoorHandle),
                    DoorHandleQuantity = doorHandle.DoorHandleQuantity
                }) ;
            }
            return doorHandleOrderResponseArry;
        }
        private List<DoorOrderResponse> GetDoorsOrderResponse(Order order)
        {
            var doorOrderResponseArray = new List<DoorOrderResponse>();

            foreach(var door in order.Doors)
            {
                doorOrderResponseArray.Add(new DoorOrderResponse()
                {
                    Door = _mapper.Map<DoorResponse>(door.Door),
                    DoorQunatity = door.DoorQuantity
                    
                });
            }
            return doorOrderResponseArray;
        }

    }
}
