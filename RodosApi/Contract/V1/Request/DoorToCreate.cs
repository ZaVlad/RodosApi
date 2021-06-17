using RodosApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RodosApi.Contract.V1.Request
{
    public class DoorToCreate
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public long DoorModelId { get; set; }
        public long CoatingId { get; set; }
        public long CollectionId { get; set; }
        public long CategoryId { get; set; }
        public long ColorId { get; set; }
        public long MakerId { get; set; }
        public long DoorHandleId { get; set; }
        public long HingesId { get; set; }
        public long TypeOfDoorId { get; set; }
    }
}
