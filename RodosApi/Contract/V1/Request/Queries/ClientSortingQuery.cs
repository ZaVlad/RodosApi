using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RodosApi.Contract.V1.Request.Queries
{
    public class ClientSortingQuery
    {
        public ClientSortingQuery()
        {
            NameSort = 0;
            ClientIdSort = 0;
            LastNameSort = 0;
            EmailSort = 0;
            AdressSort = 0;
        }
        public byte NameSort { get; set; }
        public byte ClientIdSort { get; set; }
        public byte LastNameSort { get; set; }
        public byte EmailSort { get; set; }
        public byte AdressSort { get; set; }

    }
}
