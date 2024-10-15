using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using to_do.Models.Authentication;

namespace to_do.Controllers


{   [Route("api/[controller]")]
        [ApiController]
        [Authorize]
    public class AdminController(
         UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration config
        ) : ControllerBase
    {
    
     
  
    }
}
