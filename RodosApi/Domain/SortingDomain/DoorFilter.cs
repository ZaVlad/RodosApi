using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RodosApi.Domain.SortingDomain
{
    public class DoorFilter
    {
        public string Name { get; set; }
        public long? CoatingId { get; set; }
        public long? CollectionId { get; set; }
        public long? CategoryId { get; set; }

        public long? ColorId { get; set; }
        public long? MakerId { get; set; }

        public long? DoorHandleId { get; set; }
        public long? HingesId { get; set; }

        public long? TypeOfDoorId { get; set; }
    }
}
