using BlazorApp.Models;

namespace BlazorApp.Interfaces
{
    public interface IToDoRepository
    {
        Task<IEnumerable<ToDo>> GetAll();
        Task Add(ToDo toDo);
        Task Update(int id, ToDo toDo);
        Task Delete(int id);
        Task<IEnumerable<ToDoAmount>> GetAmountDailyInAWeek();
        Task DeleteAll();
    }
}
