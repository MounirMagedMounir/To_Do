

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace to_do.Models.Authentication
{
    public class User : IdentityUser
    {


        public string Name { get; set; }

        public List<ToDoList> ToDoList { get; set; }


    }
}
