using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Manage.Internal;
using Microsoft.IdentityModel.Tokens;
using RodosApi.Domain;
using RodosApi.Options;

namespace RodosApi.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtSettings _jwtSettings;

        public IdentityService(UserManager<IdentityUser> userManager, JwtSettings jwtSettings)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings;
        }

        public async Task<AuthenticationResult> RegisterAsync(string requestName, string requestPassword, string requestEmail, string requestRole)
        {
            var registerValidation = await _userManager.FindByEmailAsync(requestEmail);
            if (registerValidation != null)
            {
                return new AuthenticationResult()
                {
                    ErrorMessages = new[] {"User with same email is already exists"}
                };
            }

            var userId = Guid.NewGuid();
            var newUser = new IdentityUser()
            {
                Id = userId.ToString(),
                Email = requestEmail,
                UserName = requestName
            };
            var createdUser = await _userManager.CreateAsync(newUser,requestPassword);
            if (createdUser.Succeeded == false)
            {
                return new AuthenticationResult()
                {
                    ErrorMessages = createdUser.Errors.Select(s => s.Description)
                };
            }

            await _userManager.AddToRoleAsync(newUser, requestRole);
            return await GenerateAuthentificationForUserAsync(newUser);
        }

        public async Task<AuthenticationResult> LoginAsync(string userEmail, string userPassword)
        {
            var loginUser = await _userManager.FindByEmailAsync(userEmail);
            if (loginUser is null)
            {
                return new AuthenticationResult()
                {
                    ErrorMessages = new[] {"user doesn't exists"}
                };
            }

            var userValidPassword = await _userManager.CheckPasswordAsync(loginUser, userPassword);
            if (userValidPassword == false)
            {
                return new AuthenticationResult()
                {
                    ErrorMessages = new[] {"user/password combination is wrong"}
                };
            }

            return await GenerateAuthentificationForUserAsync(loginUser);
        }


        private async Task<AuthenticationResult> GenerateAuthentificationForUserAsync(IdentityUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role,role));
            }
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            claims.AddRange(roleClaims);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
                Expires = DateTime.UtcNow.Add(_jwtSettings.TokenLifeTime)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthenticationResult()
            {
                Token = tokenHandler.WriteToken(token),
                Success = true
            };
        }
    }
}