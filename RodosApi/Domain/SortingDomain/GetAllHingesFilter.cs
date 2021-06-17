using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RodosApi.Domain.SortingDomain
{
    public class GetAllHingesFilter
    {
        public long? MaterialId { get; set; }
        public long? CategoryId { get; set; }
        public long? FurnitureTypeId { get; set; }
        public long? TypeOFHingesId { get; set; }
        public long? MakerId { get; set; }
        public string Name { get; set; }
    }
}
