using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using to_do.Data;
using to_do.Dto;
using to_do.Models;
using to_do.Repository;

namespace to_do.Services
{
    public class ItemServices(DataContext context, IHttpContextAccessor _httpContextAccessor) : IServices
    {
        private readonly Db db = new(context, _httpContextAccessor);
        private readonly ListServices list = new(context, _httpContextAccessor);

        public async Task<ActionResult<List<Item>>> GetAll()
        {
            var todo = await context.Items.ToListAsync();
            return todo;
        }

        public async Task<Item> Get(int id)
        {
            var todo = await context.Items.FindAsync(id);

            return todo;
        }

        public async Task<ToDoList> Create(ToDoList Newlist)
        {

            context.ToDoLists.Update(Newlist);

            await context.SaveChangesAsync();
            return await list.GetById(Newlist.Id);
        }

        public async Task<Item> Update(Item NewItems)
        {

            var dbtodo = await Get(NewItems.Id);

            dbtodo.Name = NewItems.Name;
            dbtodo.State = NewItems.State;
            dbtodo.Date = NewItems.Date;
            await context.SaveChangesAsync();
            return await Get(NewItems.Id);
        }

        public async Task<ListRespones> Delete(int id)
        {

            var dbtodo = await db.GetItem(id);


            context.Items.Remove(dbtodo);
            await context.SaveChangesAsync();
            return await list.GetAll();
        }
    }
}
