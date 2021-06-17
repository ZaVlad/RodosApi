using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RodosApi.Contract.V1.Request.Queries
{
    public class CategorySortingQuery
    {
        public CategorySortingQuery()
        {
            IdSort = 0;
            NameSort = 0;
        }
        public byte IdSort { get; set; }
        public byte  NameSort { get; set; }
    }
}
