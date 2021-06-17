using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RodosApi.Contract.V1.Request.Queries
{
    public class OrderSortingQuery
    {
        public OrderSortingQuery()
        {
            OrderIdSorting = 0;
            ClientNameSorting = 0;
            OrderDateSorting = 0;
            PriceSorting = 0;
        }
        public byte OrderIdSorting { get; set; }
        public byte ClientNameSorting { get; set; }
        public byte OrderDateSorting { get; set; }
        public byte PriceSorting { get; set; }

    }
}
