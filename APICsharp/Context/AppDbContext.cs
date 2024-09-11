using APICSharpToDoList.Models;
using APICSharpToDoList.Services;
using Microsoft.EntityFrameworkCore;

namespace APICSharpToDoList.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base (options)
        {
        }

        public DbSet<ToDo> ToDoTable { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite($"Data Source={ConfigureDataBase.DataBasePath}");
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}
