using SQLite;

namespace MAUI.Playkon.ir.V2.Models
{
    public class Response
    {
        public string id { get; set; }
        public bool status { get; set; }
        public string username { get; set; }
        public string token { get; set; }
        public string message { get; set; }
    }

    public class LoginResult
    {
        public int id { get; set; }
        public Response response { get; set; }
        public string image { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public bool isRestricted { get; set; }
        public int code { get; set; }
        public string apiVersion { get; set; }
        public string androidVersion { get; set; }
    }
    public class Account
    {
        [PrimaryKey]
        public int id { get; set; }
        public string username { get; set; }
        public string token { get; set; }
        public string image { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public bool isRestricted { get; set; }
        public int code { get; set; }
        public string apiVersion { get; set; }
        public string androidVersion { get; set; }
    }

}
