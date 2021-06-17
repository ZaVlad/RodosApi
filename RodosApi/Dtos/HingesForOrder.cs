using RodosApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RodosApi.Dtos
{
    public class HingesForOrder
    {
        public long HingesId { get; set; }
        public int  HingesQuantity { get; set; }
    }
}
