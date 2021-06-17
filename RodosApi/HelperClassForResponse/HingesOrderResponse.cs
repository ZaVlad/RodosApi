using RodosApi.Contract.V1.Response;
using RodosApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RodosApi.HelperClassForResponse
{
    public class HingesOrderResponse
    {
        public HingesResponse Hinges{ get; set; }
        public int HingesQuantity { get; set; }
    }
}
