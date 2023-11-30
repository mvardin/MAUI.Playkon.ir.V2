using SQLite;

namespace MAUI.Playkon.ir.V2.Models
{
    public class Log
    {
        [PrimaryKey]
        public string id { get; set; }
        public DateTime CreateAt { get; set; }
        public string Message { get; set; }
    }
}
