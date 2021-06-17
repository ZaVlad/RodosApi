using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RodosApi.Contract.V1.Request.Queries
{
    public class OrderFilterQuery
    {
        public OrderFilterQuery()
        {
            ClientFilterId = null;
            HingesFilterId = null;
            DeliveryStatusFilterId = null;
        }
        public long? ClientFilterId { get; set; }
        public long? HingesFilterId { get; set; }
        public byte? DeliveryStatusFilterId { get; set; }

    }
}
