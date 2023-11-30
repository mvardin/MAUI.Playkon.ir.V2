using Kotlin;
using MAUI.Playkon.ir.V2.Data;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace MAUI.Playkon.ir.V2.Services
{
    public class ApiService
    {
        private static ApiService _instance;
        public static ApiService GetInstance()
        {
            if (_instance == null)
                _instance = new ApiService();
            return _instance;
        }

        public T Get<T>(string action)
        {
            try
            {
                HttpClient client = new HttpClient();
                string token = getToken();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                return JsonConvert.DeserializeObject<T>(client.GetStringAsync("https://api.playkon.ir/api" + action).Result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public T Post<T>(string action, string data)
        {
            try
            {
                HttpClient client = new HttpClient();

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://api.playkon.ir/api" + action);

                request.Headers.Add("accept", "*/*");
                string token = getToken();
                request.Headers.Add("Authorization", "bearer " + token);

                request.Content = new StringContent(data);
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                HttpResponseMessage response = client.SendAsync(request).Result;
                response.EnsureSuccessStatusCode();
                string responseBody = response.Content.ReadAsStringAsync().Result;

                var result = JsonConvert.DeserializeObject<T>(responseBody);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string getToken()
        {
            try
            {
                var account = new AccountData().Get();
                var token = account.token;
                return token;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
    }
}