using RodosApi.Contract.V1.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RodosApi.HelperClassForResponse
{
    public class DoorOrderResponse
    {
        public DoorResponse Door{ get; set; }
        public int DoorQunatity { get; set; }
    }
}
