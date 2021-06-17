using RodosApi.Domain;
using RodosApi.Dtos;
using RodosApi.HelperClassForResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RodosApi.Contract.V1.Response
{
    public class OrderResponse
    {
        public long OrderId { get; set; }
        public ClientResponse ClientResponse { get; set; }
        public List<DoorOrderResponse> Doors { get; set; }
        public List<HingesOrderResponse> Hinges { get; set; }
        public List<DoorHandleOrderResponse> DoorHandle { get; set; }
        public DeliveryStatus DeliveryStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Price { get; set; }


    }
}
