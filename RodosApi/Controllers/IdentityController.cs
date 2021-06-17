using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RodosApi.Contract;
using RodosApi.Contract.V1.Request;
using RodosApi.Contract.V1.Response;
using RodosApi.Services;

namespace RodosApi.Controllers
{
    public class IdentityController : Controller
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost(ApiRoutes.Identity.Register)]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthFailedResponse()
                {
                    ErrorMessages = ModelState.Values.SelectMany(s => s.Errors.Select(x => x.ErrorMessage))
                });
            }
            var authResponse =
                await _identityService.RegisterAsync(request.Name, request.Password, request.Email, request.Role);

            if (authResponse.Success == false)
            {
                return BadRequest(new AuthFailedResponse()
                {
                    ErrorMessages = authResponse.ErrorMessages
                });
            }
            return Ok(new AuthSuccessResponse()
            {
                Token = authResponse.Token
            });
        }
        [HttpPost(ApiRoutes.Identity.Login)]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthFailedResponse()
                {
                    ErrorMessages = ModelState.Values.SelectMany(s => s.Errors.Select(x => x.ErrorMessage))
                });
            }

            var loginResponse = await _identityService.LoginAsync(user.Email,user.Password);
            if (loginResponse.Success == false)
            {
                return BadRequest(new AuthFailedResponse()
                {
                    ErrorMessages = loginResponse.ErrorMessages
                });
            }

            return Ok(new AuthSuccessResponse()
            {
                Token = loginResponse.Token
            });


        }
    }
}