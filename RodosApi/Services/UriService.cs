using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using RodosApi.Contract.V1.Request.Queries;

namespace RodosApi.Services
{
    public class UriService: IUriService
    {
        private readonly string _baseUri;
        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }
        public Uri GetUriForAll(PaginationQuery paginationQuery = null)
        {
            if (paginationQuery == null)
            {
                return new Uri(_baseUri);
            }

            var modifiedUri = QueryHelpers.AddQueryString(_baseUri, "pageNumber", paginationQuery.PageNumber.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", paginationQuery.PageSize.ToString());

            return new Uri(modifiedUri);
        }

        public Uri BaseUri()
        {
            return new Uri(_baseUri);
        }
    }

   
}