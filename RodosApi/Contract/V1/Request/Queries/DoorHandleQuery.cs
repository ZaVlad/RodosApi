using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RodosApi.Contract.V1.Request.Queries
{
    public class DoorHandleQuery
    {
        public DoorHandleQuery()
        {
            Name = null;
            CategoryId = null;
            MakerId = null;
            FurnitureTypeId = null;
            ColorId = null;
            MaterialId = null;
        }
        public string Name { get; set; }
        public long? CategoryId { get; set; }

        public long? MakerId { get; set; }
        public long? FurnitureTypeId { get; set; }

        public long? ColorId { get; set; }

        public long? MaterialId { get; set; }


    }

}
