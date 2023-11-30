using MAUI.Playkon.ir.V2.Models;
using SQLite;

namespace MAUI.Playkon.ir.V2.Data
{
    public class LogData
    {
        private SQLiteAsyncConnection _database;
        public LogData()
        {
            _database = new SqliteManager().GetContext();
        }
        public void Add(Log data)
        {
            _ = _database.InsertAsync(data);
        }
    }
}
