using BlazorApp.Interfaces;
using BlazorApp.Models;
using BlazorApp.Repositories;
using Blazored.LocalStorage;
using Blazored.LocalStorage.Serialization;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MudBlazor.Services;
using Radzen;

namespace BlazorApp.Services
{
    public static class ConfigureServices
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddMudServices();
            services.AddRadzenComponents();
            services.AddBlazoredLocalStorage();

            //Repositories
            services.AddScoped<IToDoRepository, ToDoRepository>();

            //HttpClients
            services.AddHttpClient(HttpServices.APICsharpClientName, x => x.BaseAddress = new Uri(HttpServices.APICsharpBaseAddress));
            services.AddHttpClient(HttpServices.APIPythonClientName, x => x.BaseAddress = new Uri(HttpServices.APIPythonBaseAddres));
        }
    }

    public static class HttpServices
    {
        public static string APICsharpClientName = "apicsharp";
        //public static string APICsharpBaseAddress = "https://api-csharp-latest.onrender.com/api/ToDo/";
        public static string APICsharpBaseAddress = "http://localhost:8080/api/ToDo/";
        public static string APIPythonClientName = "apipython";
        public static string APIPythonBaseAddres = "";
    }
}
