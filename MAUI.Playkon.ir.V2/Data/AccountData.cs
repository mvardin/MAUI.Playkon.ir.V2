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
            var list = _database.Table<Account>().ToListAsync().Result;
            var account = list.FirstOrDefault();
            return account;
        }
    }
}
