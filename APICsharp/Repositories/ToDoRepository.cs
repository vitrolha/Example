using APICSharpToDoList.Context;
using APICSharpToDoList.Interfaces;
using APICSharpToDoList.Models;
using APICSharpToDoList.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICSharpToDoList.Repositories
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly AppDbContext context;
        public ToDoRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task Create(ToDoDTO toDoDTO)
        {
            if (context.ToDoTable == null) throw new Exception("ToDo entity is null.");

            var result = context.Add(new ToDo { Title = toDoDTO.Title, Start = toDoDTO.Start, End = toDoDTO.End });

            await context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            if (context.ToDoTable == null) throw new Exception("ToDo entity is null.");

            var todo = context.ToDoTable.Find(id);

            if (todo != null)
            {
                context.ToDoTable.Remove(todo);

                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ToDo>> GetAll()
        {
            if (context.ToDoTable == null) throw new Exception("ToDo entity is null.");

            return await context.ToDoTable.ToListAsync();
        }

        public async Task<ToDo> GetById(int id)
        {
            if (context.ToDoTable == null) throw new Exception("ToDo entity is null.");

            return await context.ToDoTable.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Update(int id, ToDo toDo)
        {
            if (context.ToDoTable == null) throw new Exception("ToDo entity is null.");

            var td = context.ToDoTable.Find(id);

            if (td != null)
            {
                td.Title = toDo.Title;

                td.Start = toDo.Start;

                td.End = toDo.End;

                context.Entry(td).State = EntityState.Modified;

                await context.SaveChangesAsync();
            }
        }

        public async Task<int> DeleteAllData()
        {
            if (context.ToDoTable == null) throw new Exception("ToDo entity is null.");

            var dataToDelete = await context.ToDoTable.ToListAsync();

            var i = dataToDelete.Count;

            context.ToDoTable.RemoveRange(dataToDelete);
            await context.SaveChangesAsync();

            return i;
        }
    }
}
