using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RodosApi.Contract.V1.Request.Queries
{
    public class MakerFilterQuery
    {
        public MakerFilterQuery()
        {
            Name = null;
            CountryId = null;
        }
        public string Name { get; set; }
        public long? CountryId { get; set; }
    }
}
