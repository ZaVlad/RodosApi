using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RodosApi.Domain.SortingDomain
{
    public class DoorHandleFilter
    {
        public string Name { get; set; }
        public long? CategoryId { get; set; }

        public long? MakerId { get; set; }
        public long? FurnitureTypeId { get; set; }

        public long? ColorId { get; set; }

        public long? MaterialId { get; set; }
    }
}
