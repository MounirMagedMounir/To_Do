using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using to_do.Data;
using to_do.Dto;
using to_do.Models;
using to_do.Models.Authentication;

namespace to_do.Repository
{
    public class ListServices(DataContext context, IHttpContextAccessor httpContextAccessor) : IServices
    {
        private readonly Db db = new(context, httpContextAccessor);

        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;


        public async Task<ListRespones> GetAll()
        {
            var userToDoList = await db.GetLoggedInUser();

            return new ListRespones(userToDoList?.Id, userToDoList?.Name, userToDoList?.ToDoList, userToDoList?.ToDoList.Count);
        }

        public async Task<ToDoList> GetById(int id)
        {

            var list = await context.ToDoLists.Include(i => i.Items).FirstOrDefaultAsync(i => i.Id == id);
            return list;

        }
        public async Task<ToDoList> Create(ToDoList request)
        {
            var userToDoList = await db.GetLoggedInUser();

            var Newlist = new ToDoList { Id = request.Id, Name = request.Name, };

            var NewItems = request.Items.Select(i => new Item { Id = i.Id, Date = i.Date, Name = i.Name, State = i.State }).ToList();

            Newlist.Items = NewItems;

            userToDoList?.ToDoList.Add(Newlist);

            await context.SaveChangesAsync();
            return await GetById(Newlist.Id);
        }

        public async Task<ToDoList> Update(ToDoList Newlist)
        {

            var list = await context.ToDoLists.FindAsync(Newlist.Id);

            list.Name = Newlist.Name;
            if (list.Items is null)
            {
                list.Items = Newlist.Items;
            }
            else
            {
                foreach (var i in Newlist.Items)
                {


                    var item = await context.Items.FindAsync(i.Id);



                    if (i.Id == 0)
                    {
                        list.Items.Add(new Item { Id = i.Id, Name = i.Name, State = i.State, Date = i.Date });
                    }
                    else
                    {
                        item.Id = i.Id;
                        item.Name = i.Name;
                        item.State = i.State;
                        item.Date = i.Date;
                    }
                }
            };




            await context.SaveChangesAsync();
            return await GetById(Newlist.Id);

        }



        public async Task<ListRespones> Delete(int id)
        {

            var list = await db.GetList(id);
            list.Items.ForEach(async i => await db.DeleteItem(i.Id));



            context.ToDoLists.Remove(list);
            await context.SaveChangesAsync();
            return await GetAll();

        }


    }
}

