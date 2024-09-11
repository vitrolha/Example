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
        }
    }
}
