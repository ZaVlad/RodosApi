using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using RodosApi.Contract.V1.Request.Queries;

namespace RodosApi.Services
{
    public interface IUriService
    {
        public Uri GetUriForAll(PaginationQuery paginationQuery = null);
        public Uri BaseUri();
    }
}
