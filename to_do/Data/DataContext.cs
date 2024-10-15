using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using to_do.Models;
using to_do.Models.Authentication;

namespace to_do.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {

        }

        public DbSet<Item> Items { get; set; }
        public DbSet<ToDoList> ToDoLists { get; set; }
        //public DbSet<User> User { get; set; }
    }
}
