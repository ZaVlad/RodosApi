using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RodosApi.Contract.V1.Request.Queries;
using RodosApi.Contract.V1.Response;
using RodosApi.Domain;
using RodosApi.Services;

namespace RodosApi.Helpers
{
    public  class PaginationHelpers
    {
        public static object CreatePaginationResponse<T>(IUriService uri,PaginationFilter pagination,List<T> response)
        {
            var nextPage = pagination.PageNumber >= 1
                ?uri.GetUriForAll(new PaginationQuery(pagination.PageNumber + 1, pagination.PageSize)).ToString() : null;
            var previousPage = pagination.PageNumber - 1 >= 1
                ? uri.GetUriForAll(new PaginationQuery(pagination.PageNumber - 1, pagination.PageSize)).ToString() : null;

            return  new PagedResponse<T>
            {
                Data = response,
                PageSize = pagination.PageSize >= 1 ? pagination.PageSize : (int?)null,
                PageNumber = pagination.PageNumber >= 1 ? pagination.PageNumber : (int?)null,
                PreviousPage = previousPage,
                NextPage = response.Any() ? nextPage : null
            };
        }  

    }
}
