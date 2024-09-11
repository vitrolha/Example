using BlazorApp.Interfaces;
using BlazorApp.Models;
using Blazored.LocalStorage;
using Newtonsoft.Json;
using System.Globalization;

namespace BlazorApp.Repositories
{
    public class ToDoRepository : IToDoRepository
    {
        //No caso aqui seria feita as requisições paras as apis python e c#

        //Fazer com que use localstorage

        //IMPLEMENTAR LOCALSTORAGE
        private readonly ILocalStorageService localStorage;

        //Key do localstorage
        private const string todoKey = "todo-data";

        public ToDoRepository(ILocalStorageService localStorage)
        {
            this.localStorage = localStorage;
        }

        private async Task<List<ToDo>> GetToDoListFromLocalStorage()
        {
            var data = await localStorage.GetItemAsStringAsync(todoKey) ?? "";
            return JsonConvert.DeserializeObject<List<ToDo>>(data) ?? new List<ToDo>();
        }

        public async Task Add(ToDo toDo)
        {
            int id = 1;
            var list = await GetToDoListFromLocalStorage();

            if (list.Count <= 0) list.Add(new ToDo { Id = id, Title = toDo.Title, Start = toDo.Start, End = toDo.End });

            else
            {
                int lastId = list.OrderBy(x => x.Id).Last().Id;
                list.Add(new ToDo { Id = lastId + 1, Title = toDo.Title, Start = toDo.Start, End = toDo.End });
            }

            await localStorage.SetItemAsStringAsync(todoKey, JsonConvert.SerializeObject(list));
        }

        public async Task<IEnumerable<ToDo>> GetAll()
        {
            var list = await GetToDoListFromLocalStorage();
            return list.AsEnumerable();
        }

        public async Task Delete(int id)
        {
            var list = await GetToDoListFromLocalStorage();

            if (list.Count <= 0) return;
            ToDo toDoToRemove = list.Find(x => x.Id == id) ?? throw new Exception($"ToDo with id {id} not found.");
            list.Remove(toDoToRemove);

            await localStorage.SetItemAsStringAsync(todoKey, JsonConvert.SerializeObject(list));
        }

        public async Task Update(int id, ToDo toDo)
        {
            var list = await GetToDoListFromLocalStorage();

            if (list.Count <= 0) return;
            ToDo toDoToUpdate = list.Find(x => x.Id == id) ?? throw new Exception($"ToDo with id {id} not found.");
            toDoToUpdate.Title = toDo.Title;
            toDoToUpdate.Start = toDo.Start;
            toDoToUpdate.End = toDo.End;

            await localStorage.SetItemAsStringAsync(todoKey, JsonConvert.SerializeObject(list));
        }

        public async Task<IEnumerable<ToDoAmount>> GetAmountDailyInAWeek()
        {
            var list = await GetToDoListFromLocalStorage();

            var toDoThisWeek = list.Where(x => x.Start >= DateTime.Now.Date && x.Start < DateTime.Now.AddDays(7).Date);

            var amount = toDoThisWeek.GroupBy(x => x.Start)
                                     .Select(x => new { Date = x.Key, Counter = x.Count() })
                                     .ToList()
                                     .OrderBy(x => x.Date);

            List<ToDoAmount> res = new();
            
            foreach(var item in amount)
            {
                res.Add(new ToDoAmount { Date = item.Date.Date.ToString("yyyy-MM-dd"), Amount = item.Counter });
            }

            //foreach(var item in res)
            //{
            //    Console.WriteLine("Data: " + item.Date + " | " + "Qntd: " + item.Amount);
            //}

            return res;
        }

        public async Task DeleteAll()
        {
            Console.WriteLine("Cleaning localstorage...");
            await localStorage.ClearAsync();
        }

    }
}
