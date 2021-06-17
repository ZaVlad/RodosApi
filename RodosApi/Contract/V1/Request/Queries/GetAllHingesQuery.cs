using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RodosApi.Contract.V1.Request.Queries
{
    public class GetAllHingesQuery
    {
        public GetAllHingesQuery()
        {
            MaterialId = null;
            CategoryId = null;
            FurnitureTypeId = null;
            Name = null;
            TypeOFHingesId = null;
            MakerId = null;
        }
        public long? MaterialId { get; set; }
        public long? CategoryId { get; set; }
        public long? FurnitureTypeId { get; set; }
        public long? TypeOFHingesId { get; set; }
        public long? MakerId { get; set; }
        public string Name { get; set; }
    }
}
