using MAUI.Playkon.ir.V2.Models;
using SQLite;

namespace MAUI.Playkon.ir.V2.Data
{
    public class SqliteManager
    {
        private string _databasePath;
        public SQLiteAsyncConnection GetContext()
        {
            try
            {
                _databasePath = Path.Combine(FileSystem.AppDataDirectory, "Playkon.db3");
                if (!File.Exists(_databasePath))
                {
                    var database = new SQLiteAsyncConnection(_databasePath, SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite);
                    _ = database.CreateTableAsync<Music>().Result;
                    _ = database.CreateTableAsync<Log>().Result;
                    _ = database.CreateTableAsync<Account>().Result;
                    return database;
                }
                else
                {
                    var database = new SQLiteAsyncConnection(_databasePath, SQLiteOpenFlags.ReadWrite);
                    return database;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
