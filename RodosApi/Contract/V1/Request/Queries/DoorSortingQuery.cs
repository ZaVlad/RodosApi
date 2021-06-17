using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RodosApi.Contract.V1.Request.Queries
{
    public class DoorSortingQuery
    {
        public DoorSortingQuery()
        {
            IdSort = 0;
            NameSort = 0;
            PriceSort = 0;
            DoorModelNameSort = 0;
            CoatingNameSort = 0;
            CollectionNameSort = 0;
            ColorNameSort = 0;
            MakerNameSort = 0;
            DoorHandleNameSort = 0;
            HingesNameSort = 0;
        }
        public byte IdSort { get; set; }
        public byte NameSort { get; set; }
        public byte PriceSort { get; set; }
        public byte DoorModelNameSort { get; set; }
        public byte CoatingNameSort { get; set; }
        public byte CollectionNameSort { get; set; }
        public byte ColorNameSort { get; set; }
        public byte MakerNameSort { get; set; }
        public byte DoorHandleNameSort { get; set; }
        public byte HingesNameSort { get; set; }

    }
}
