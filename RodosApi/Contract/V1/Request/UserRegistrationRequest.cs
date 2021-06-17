using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RodosApi.Contract.V1.Request
{
    public class UserRegistrationRequest
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        [PasswordPropertyText]
        public string Password { get; set; }
    }
}