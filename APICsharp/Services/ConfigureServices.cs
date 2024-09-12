using APICSharpToDoList.Context;
using APICSharpToDoList.Interfaces;
using APICSharpToDoList.Repositories;
using Microsoft.EntityFrameworkCore;

namespace APICSharpToDoList.Services
{
    public static class ConfigureServices
    {
        public static void Configure(IServiceCollection services) 
        {
            //Cors Conf
            services.AddCors(x => x.AddPolicy(CorsConf.CorsPolicyName, policy => policy
                                                                        .WithOrigins([CorsConf.BaseAddresBackEnd, CorsConf.BaseAddresFrontEnd, CorsConf.APIPythonBaseAddres, CorsConf.BaseAddressFrontEndLocalHost])
                                                                        .AllowAnyHeader()
                                                                        .AllowAnyMethod()
                                                                        .AllowCredentials()));


            //Exemplo
            //services.AddDbContext<AppDbContext>(x => x.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            //"DefaultConnection": é a string de conexao do banco
            services.AddDbContext<AppDbContext>(x => x.UseSqlite(ConfigureDataBase.ConnectionString));

            services.AddScoped<IToDoRepository, ToDoRepository>();
      
        }
    }

    public class CorsConf
    {
        public static string CorsPolicyName = "wasm";
        public static string BaseAddresBackEnd = "https://localhost:7156";
        public static string BaseAddresFrontEnd = "https://vitrolha.github.io";
        public static string APIPythonBaseAddres = "http://127.0.0.1:5000";
        public static string BaseAddressFrontEndLocalHost = "https://localhost:7025";
    }
}
