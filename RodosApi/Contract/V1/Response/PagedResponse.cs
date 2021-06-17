using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RodosApi.Contract.V1.Response
{
    public class PagedResponse<T>
    {
        public PagedResponse() { }

        public PagedResponse(IEnumerable<T> data)
        {
            Data = data;

        }

        public IEnumerable<T> Data { get; set; }
        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }
        public string NextPage { get; set; }
        public string PreviousPage { get; set; }
    }
}
