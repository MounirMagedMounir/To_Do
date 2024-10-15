using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using to_do.Data;
using to_do.Dto;
using to_do.Models;
using to_do.Models.Authentication;
using to_do.Repository;

namespace to_do.Utils
{
    public class ValidationtUtils(DataContext? context, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
    {

        private readonly Db db = new(context, httpContextAccessor);

        public async Task<bool> CheckList(int id)
        {
            var userToDoList = await db.GetLoggedInUser();
            if (userToDoList?.ToDoList.FirstOrDefault(i => i.Id == id) is null)
                return false;
            else return true;
        }
        public async Task<bool> CheckItem(int id)
        {
            var userToDoList = await db.GetLoggedInUser();

            if (userToDoList.ToDoList.FirstOrDefault(i => i.Items.Any(j => j.Id == id)) is null)
                return false;
            else return true;
        }
        public async Task<bool> CheckUserEmail(string Email)
        {

            var user = await userManager.FindByEmailAsync(Email);

            if (user is null)
                return false;
            else return true;
        }
        public async Task<bool> CheckUserPassword(User user, string Password)
        {

            bool checkUserPassword = await userManager.CheckPasswordAsync(user, Password);

            return checkUserPassword;
        }

        //public async Task<ToDoList> CheckUser(int id)
        //{
        //           var UserID = string.Empty;
        //if (_httpContextAccessor.HttpContext is not null)
        //{
        //    UserID = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        //}
        //}
    }
}
