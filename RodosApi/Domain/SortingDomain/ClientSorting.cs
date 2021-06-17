using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RodosApi.Domain.SortingDomain
{
    public class ClientSorting
    {
        public byte NameSort { get; set; }
        public byte ClientIdSort { get; set; }
        public byte LastNameSort { get; set; }
        public byte EmailSort { get; set; }
        public byte AdressSort { get; set; }
    }
}
