using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using RodosApi.Domain;

namespace RodosApi.Services
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> RegisterAsync(string requestName, string requestPassword, string requestEmail, string requestRole);
        Task<AuthenticationResult> LoginAsync(string userEmail, string userPassword);
    }
}