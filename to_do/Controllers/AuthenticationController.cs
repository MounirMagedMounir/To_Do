

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using to_do.Data;
using to_do.Dto;
using to_do.Models.Authentication;
using to_do.Repository;
using to_do.Utils;

namespace to_do.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(
         UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration config, IHttpContextAccessor _httpContextAccessor
        ) : ControllerBase
    {
        private readonly AuthServices _authServices = new AuthServices(userManager, roleManager, config, _httpContextAccessor);
        private readonly ValidationtUtils utils = new ValidationtUtils(null, _httpContextAccessor, userManager);

        [HttpPost("register")]
        public async Task<GeneralAuthResponse> RegisterUser(Register userDTO)
        {
            if (await utils.CheckUserEmail(userDTO.Email)) return new GeneralAuthResponse(StatusCodes.Status409Conflict, "User Already excests ", null);
            else
            {
                var register = await _authServices.RegisterUser(userDTO);
                return (register);
            }
        }




        [HttpPost("login")]

        public async Task<GeneralAuthResponse> LoginAccount(LogIn loginDTO)
        {

            if (!await utils.CheckUserEmail(loginDTO.Email))
                return new GeneralAuthResponse(StatusCodes.Status401Unauthorized, "User dosen't excests ", null);
            else
            {
                var LogIn = await _authServices.LoginAccount(loginDTO);
                return LogIn;
            }
        }
        [HttpPost("refreshToken")]
        [Authorize]
        public async Task<GeneralAuthResponse> RefreshToken()
        {

            var RefreshToken = await _authServices.RefreshToken();
            return RefreshToken;
        }

    }
}
