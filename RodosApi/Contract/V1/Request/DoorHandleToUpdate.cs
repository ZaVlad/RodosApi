using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RodosApi.Contract.V1.Request
{
    public class DoorHandleToUpdate
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public long CategoryId { get; set; }
        public long MakerId { get; set; }
        public long FurnitureTypeId { get; set; }
        public long ColorId { get; set; }
        public long MaterialId { get; set; }
    }
}
