using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using to_do.Data;
using to_do.Dto;
using to_do.Models;
using to_do.Models.Authentication;
using to_do.Repository;
using to_do.Services;
using to_do.Utils;
using static to_do.Dto.ManyIds;
namespace to_do.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class ItemsController(DataContext context, IHttpContextAccessor _httpContextAccessor, UserManager<User> userManager, RoleManager<IdentityRole> roleManager) : ControllerBase
    {
        private readonly Db db = new(context, _httpContextAccessor);
        private readonly ItemServices item = new(context, _httpContextAccessor);
        private readonly ListServices list = new(context, _httpContextAccessor);
        private readonly ValidationtUtils validationt = new(context, _httpContextAccessor, userManager);



        [HttpPost("Create/")]
        public async Task<ActionResult<ToDoList>> Create(ToDoList NewList)
        {
            return Ok(await item.Create(NewList));
        }



        [HttpPost("Update/")]
        public async Task<ActionResult<ToDoList>> Update(Item NewItem)
        {
            if (!await validationt.CheckItem(NewItem.Id))
            {
                return NotFound($"ther is no ToDo Item with id {NewItem.Id}");
            }
            else
            {
                return Ok(await item.Update(NewItem));
            }
        }


        [HttpDelete("Delete/")]
        public async Task<ActionResult<List<ToDoList>>> Delete(int id)
        {
            if (!await validationt.CheckItem(id))
            {

                return NotFound($"ther is no to do item with id {id}");
            }
            else
            {
                return Ok(await item.Delete(id));
            }
        }

        [HttpDelete("DeleteMany/")]
        public async Task<ActionResult<List<ToDoList>>> DeleteMany(Ids ids)
        {
            var res = StatusCode(400, "there is no ids (null) ");

            if (!ids.ids.IsNullOrEmpty())
            {
                foreach (var id in ids.ids)
                {


                    if (!await validationt.CheckItem(id))
                    {
                        res = NotFound($"there is no to do list with id {id}");
                    }
                    else
                    {
                        res = Ok(await item.Delete(id));
                    }

                }

            }

            return res;
        }
    }
}
