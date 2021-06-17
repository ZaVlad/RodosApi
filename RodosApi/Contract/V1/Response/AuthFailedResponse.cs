using System.Collections.Generic;

namespace RodosApi.Contract.V1.Response
{
    public class AuthFailedResponse
    {
        public IEnumerable<string> ErrorMessages { get; set; }

    }
}