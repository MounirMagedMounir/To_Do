using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using to_do.Dto;
using to_do.Models.Authentication;
using to_do.Utils;

namespace to_do.Repository
{
    public class AuthServices(
         UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration config, IHttpContextAccessor _httpContextAccessor
        ) : IServices
    {


        private readonly AuthUtils authUtils = new AuthUtils(config);
        private readonly ValidationtUtils utils = new ValidationtUtils(null, _httpContextAccessor, userManager);
        public async Task<GeneralAuthResponse> RegisterUser(Register userDTO)
        {

            var newUser = new User()
            {
                Name = userDTO.Name,
                Email = userDTO.Email,
                PasswordHash = userDTO.Password,
                UserName = userDTO.Name
            };


            var user = await userManager.FindByEmailAsync(newUser.Email);

            var createUser = await userManager.CreateAsync(newUser!, userDTO.Password);
            if (!createUser.Succeeded) return new GeneralAuthResponse(StatusCodes.Status500InternalServerError, $"{createUser}", null);

            await userManager.AddToRoleAsync(newUser, "User");
            return new GeneralAuthResponse(StatusCodes.Status201Created, "succes", null);




        }

        public async Task<GeneralAuthResponse> LoginAccount(LogIn loginDTO)
        {

            var getUser = await userManager.FindByEmailAsync(loginDTO.Email);


            if (!await utils.CheckUserPassword(getUser, loginDTO.Password))
           

                return new GeneralAuthResponse(StatusCodes.Status403Forbidden, "wrong password", null);
          
                var getUserRole = await userManager.GetRolesAsync(getUser);
                var userSession = new UserSession(getUser.Id, getUser.Name, getUser.Email, getUserRole.First());

                string token = authUtils.GenerateToken(userSession);

                return new GeneralAuthResponse(StatusCodes.Status200OK, "succes", $"{token}");
            
        }

        public async Task<GeneralAuthResponse> RefreshToken()
        {
            var UserEmail = string.Empty;
            var userToken = string.Empty;
            if (_httpContextAccessor.HttpContext is not null)
            {
                userToken = _httpContextAccessor.HttpContext.User.FindFirstValue("exp");
                UserEmail = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);


            }

            if (userToken is not null)
            {
                DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                TimeSpan timeSpan = TimeSpan.FromSeconds(int.Parse(userToken));

                DateTime convertedTime = epoch.AddSeconds(timeSpan.TotalSeconds);

                DateTime start = DateTimeOffset.FromUnixTimeSeconds(int.Parse(userToken)).DateTime;
                var startUtc = DateTime.SpecifyKind(start, DateTimeKind.Utc);

                if (convertedTime > DateTime.Now)
                {
                    if (UserEmail is not null)
                    {
                        var user = await userManager.FindByEmailAsync(UserEmail);
                        if (user is null)
                            return new GeneralAuthResponse(StatusCodes.Status403Forbidden, "failed", null);

                        var getUserRole = await userManager.GetRolesAsync(user);
                        var userSession = new UserSession(user.Id, user.Name, user.Email, getUserRole.First());


                        var RefreshToken = authUtils.GenerateToken(userSession);
                        return new GeneralAuthResponse(StatusCodes.Status200OK, "succes", $"{RefreshToken}");
                    }
                    else
                    {
                        return new GeneralAuthResponse(StatusCodes.Status403Forbidden, "failed", null);

                    }
                }
                else
                {
                    return new GeneralAuthResponse(StatusCodes.Status403Forbidden, "failed", null);

                }
            }
            else
            {
                return new GeneralAuthResponse(StatusCodes.Status403Forbidden, "failed", null);

            }
        }


    }
}
