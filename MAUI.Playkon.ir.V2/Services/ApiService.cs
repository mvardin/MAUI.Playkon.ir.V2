﻿using CommunityToolkit.Maui.Alerts;
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
                Shell.Current.DisplaySnackbar("Network error");
                return default(T);
            }
        }

        public async Task<T> Post<T>(string action, string data)
        {
            try
            {
                HttpClient client = new HttpClient();

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://api.playkon.ir/api" + action);

                request.Headers.Add("accept", "*/*");
                string token = getToken();
                if (!string.IsNullOrEmpty(token))
                    request.Headers.Add("Authorization", "bearer " + token);

                request.Content = new StringContent(data);
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                HttpResponseMessage response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<T>(responseBody);

                return result;
            }
            catch (Exception ex)
            {
                Shell.Current.DisplaySnackbar("Network error");
                return default(T);
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