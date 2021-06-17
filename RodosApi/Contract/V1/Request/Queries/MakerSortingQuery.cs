using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RodosApi.Contract.V1.Request.Queries
{
    public class MakerSortingQuery
    {
        public MakerSortingQuery()
        {
            IdSort = 0;
            NameSort = 0;
            CountryNameSort = 0;
          
        }
        public byte IdSort { get; set; }
        public byte NameSort { get; set; }
        public byte CountryNameSort { get; set; }
    }
}
