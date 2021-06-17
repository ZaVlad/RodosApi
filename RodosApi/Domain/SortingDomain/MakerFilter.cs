using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RodosApi.Domain.SortingDomain
{
    public class MakerFilter
    {
        public string Name { get; set; }
        public long? CountryId { get; set; }
    }
}
