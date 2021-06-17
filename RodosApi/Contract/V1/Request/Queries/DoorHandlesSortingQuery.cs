using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RodosApi.Contract.V1.Request.Queries
{
    public class DoorHandlesSortingQuery
    {
        public DoorHandlesSortingQuery()
        {
            IdSort = 0;
            NameSort = 0;
            PriceSort = 0;
            MakerNameSort = 0;
            ColorNameSort = 0;
            MaterialNameSort = 0;
        }
        public byte  IdSort { get; set; }
        public byte NameSort { get; set; }
        public byte PriceSort { get; set; }
        public byte MakerNameSort { get; set; }
        public byte ColorNameSort { get; set; }
        public byte MaterialNameSort { get; set; }
    }
}
