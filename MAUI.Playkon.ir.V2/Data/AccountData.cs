using MAUI.Playkon.ir.V2.Models;
using SQLite;

namespace MAUI.Playkon.ir.V2.Data
{
    public class AccountData
    {
        private SQLiteAsyncConnection _database;
        public AccountData()
        {
            _database = new SqliteManager().GetContext();
        }

        public void Add(Account data)
        {
            _ = _database.InsertOrReplaceAsync(data);
        }
        public Account Get()
        {
            return _database.Table<Account>().FirstOrDefaultAsync().Result;
        }
    }
}
