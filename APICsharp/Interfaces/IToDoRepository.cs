using APICSharpToDoList.Models;
using Microsoft.AspNetCore.Mvc;
using APICSharpToDoList.Models.DTOs;

namespace APICSharpToDoList.Interfaces
{
    public interface IToDoRepository
    {
        Task Create(ToDoDTO toDo);
        Task Update(int id, ToDo toDo);
        Task Delete(int id);
        Task<IEnumerable<ToDo>> GetAll();
        Task<ToDo> GetById(int id);
    }
}
