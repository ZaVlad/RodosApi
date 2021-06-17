using RodosApi.Contract.V1.Request.Interfaces;
using RodosApi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RodosApi.Contract.V1.Request
{
    public class OrderToUpdate:IOrderOperations
    {
        public long ClientId { get; set; }
        public List<HingesForOrder> Hinges { get; set; }
        public List<DoorHandleForOrder> DoorHandles { get; set; }
        public List<DoorForOrder> Doors { get; set; }
        public int DeliveryStatusId { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
