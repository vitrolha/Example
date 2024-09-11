using Microsoft.Data.Sqlite;

namespace APICSharpToDoList.Services
{
    public static class ConfigureDataBase
    {
        public static string DataBasePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "DataBase.db");
        public static string ConnectionString = $"Data Source={DataBasePath}";
        public static void Create()
        {
            try
            {
                using (var conn = new SqliteConnection(ConnectionString))
                {
                    conn.Open();

                    var tableCmd = @"CREATE TABLE IF NOT EXISTS ToDoTable(
                                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                    Title TEXT NOT NULL,
                                    Start TEXT NOT NULL,
                                    End TEXT NOT NULL);";

                    using (var cmd = new SqliteCommand(tableCmd, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error create DataBase :" + ex.Message);
            }
        }
    }
}
