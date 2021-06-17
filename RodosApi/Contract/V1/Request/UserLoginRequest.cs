using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RodosApi.Contract.V1.Request
{
    public class UserLoginRequest
    {
        [EmailAddress]
        public string Email { get; set; }

        [PasswordPropertyText]
        public string Password { get; set; }
    }
}