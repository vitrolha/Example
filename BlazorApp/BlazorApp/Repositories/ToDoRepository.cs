using BlazorApp.Interfaces;
using BlazorApp.Models;
using BlazorApp.Services;
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

        //Usando API publicada
        private readonly IHttpClientFactory httpClientFactory;

        private HttpClient csharpAPI;

        public ToDoRepository(ILocalStorageService localStorage, IHttpClientFactory httpClientFactory)
        {
            this.localStorage = localStorage;
            this.httpClientFactory = httpClientFactory;
            csharpAPI = httpClientFactory.CreateClient(HttpServices.APICsharpClientName);
        }

        private async Task<List<ToDo>> GetToDoListFromBd()
        {
            var data = await csharpAPI.GetAsync(csharpAPI.BaseAddress + "getall");
            return JsonConvert.DeserializeObject<List<ToDo>>(await data.Content.ReadAsStringAsync()) ?? new List<ToDo>();
        }

        public async Task Add(ToDoCreate toDo)
        {
            var data = JsonConvert.SerializeObject(toDo);
            StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            await csharpAPI.PostAsync(csharpAPI.BaseAddress + "create", content);
        }

        public async Task<IEnumerable<ToDo>> GetAll()
        {
            var list = await GetToDoListFromBd();
            return list.AsEnumerable();
        }

        public async Task Delete(int id)
        {
            await csharpAPI.DeleteAsync(csharpAPI.BaseAddress + $"delete/{id}");
        }

        public async Task Update(int id, ToDo toDo)
        {
            var data = JsonConvert.SerializeObject(toDo);
            StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            await csharpAPI.PutAsync(csharpAPI.BaseAddress + $"update/{id}", content);
        }

        public async Task<IEnumerable<ToDoAmount>> GetAmountDailyInAWeek()
        {
            //API em python aqui

            var list = await GetToDoListFromBd();

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
            //Implementar na api csharp
            Console.WriteLine("Cleaning database...");
            await csharpAPI.DeleteAsync(csharpAPI.BaseAddress + "deletealldata");
        }

    }
}
