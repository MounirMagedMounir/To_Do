using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using to_do.Data;
using to_do.Models;
using to_do.Models.Authentication;

namespace to_do.Repository
{
    public class Db
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public Db(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<User> GetLoggedInUser()
        {
            var UserID = string.Empty;
            if (_httpContextAccessor.HttpContext is not null)
            {
                UserID = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            }

            var userToDoList = await _context.Users.Include(i => i.ToDoList).ThenInclude(i => i.Items).FirstOrDefaultAsync(i => i.Id == UserID);

            return userToDoList;
        }
        public async Task<Item> GetItem(int id)
        {
            var todo = await _context.Items.FindAsync(id);

            return todo;
        }
        public async Task<ToDoList> GetList(int id)
        {

            var list = await _context.ToDoLists.Include(i => i.Items).FirstOrDefaultAsync(i => i.Id == id);

            return list;

        }
        public async Task DeleteItem(int id)
        {

            var item = await GetItem(id);


            _context.Items.Remove(item);


        }


    }

}
