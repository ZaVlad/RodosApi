using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RodosApi.Domain.SortingDomain
{
    public class OrderFilter
    {
        public long? ClientFilterId { get; set; }
        public long? HingesFilterId { get; set; }
        public byte? DeliveryStatusFilterId { get; set; }

    }
}
