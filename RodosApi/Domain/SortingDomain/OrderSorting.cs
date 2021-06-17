using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RodosApi.Domain.SortingDomain
{
    public class OrderSorting
    {
        public byte OrderIdSorting { get; set; }
        public byte ClientNameSorting { get; set; }
        public byte OrderDateSorting { get; set; }
        public byte PriceSorting { get; set; }

    }
}
