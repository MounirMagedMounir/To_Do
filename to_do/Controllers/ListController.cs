using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;
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
    public class ListController(DataContext context, IHttpContextAccessor _httpContextAccessor, UserManager<User> userManager, RoleManager<IdentityRole> roleManager) : ControllerBase
    {
        private readonly Db db = new(context, _httpContextAccessor);
        private readonly ItemServices item = new(context, _httpContextAccessor);
        private readonly ListServices list = new(context, _httpContextAccessor);
        private readonly ValidationtUtils validationt = new(context, _httpContextAccessor, userManager);
        [HttpGet("GetAll/")]

        public async Task<ListRespones> GetAll()
        {
            var allToDo = await list.GetAll();
            return allToDo;
        }


        [HttpGet("GetByID/{id}")]
        public async Task<ActionResult<ToDoList>> GetById(int id)
        {
            if (!await validationt.CheckList(id))
            {
                return NotFound($"No List with the id  {id}");
            }
            else
            {
                var todos = await list.GetById(id);
                return Ok(todos);
            }
        }


        [HttpPost("Create/")]
        public async Task<ActionResult<List<ToDoList>>> Create(ToDoList NewList)
        {
            return Ok(await list.Create(NewList));
        }


        [HttpPut("Update/")]
        public async Task<ActionResult<ToDoList>> Update(ToDoList NewList)
        {

            if (!await validationt.CheckList(NewList.Id))
            {
                return NotFound($"there is no to do list with id {NewList.Id}");
            }
            else
            {
                return Ok(await list.Update(NewList));
            }
        }



        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<ListRespones>> Delete(int id)
        {
            if (!await validationt.CheckList(id))
            {
                return NotFound($"there is no to do list with id {id}");
            }
            else
            {
                return Ok(await list.Delete(id));
            }
        }


        [HttpDelete("DeleteMany/")]
        public async Task<ActionResult<ListRespones>> DeleteMany(Ids ids)
        {

            var res = StatusCode(400, "there is no ids (null) ");

            if (!ids.ids.IsNullOrEmpty())
            {
                foreach (var id in ids.ids)
                {


                    if (!await validationt.CheckList(id))
                    {
                        res = NotFound($"there is no to do list with id {id}");
                    }
                    else
                    {
                        res = Ok(await list.Delete(id));
                    }
                }

            }

            return res;

        }



    }
}
