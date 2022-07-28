using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WeatherTest.Models;
using Xamarin.Essentials;

namespace WeatherTest.Services
{
    public class HttpService
    {

        public static HttpClient HttpClient = new HttpClient();

        public static async Task<Response<T>> SendRequest<T>(HttpMethod httpMethod, string url, string data_map = "")
        {

            var current = Connectivity.NetworkAccess;

            if (current != NetworkAccess.Internet)
            {
                return new Response<T>()
                {
                    Success = false,
                    ErrorMessage = "Internet connection is required."
                };
            }

            Debug.WriteLine($"Calling: {url}");

            var request = new HttpRequestMessage(httpMethod, new Uri(url));

            var response = await HttpClient.SendAsync(request);

            var data = response.Content.ReadAsStringAsync().Result;

            try
            {

                var formatted_body = JsonConvert.SerializeObject(JsonConvert.DeserializeObject(data), Formatting.Indented);
                Debug.WriteLine(formatted_body);
                Debug.WriteLine("Endpoint: " + url);


            }
            catch (Exception ex)
            {
                return new Response<T>()
                {
                    Success = false,
                    ErrorMessage = "An error has occurred."
                };
            }
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return new Response<T>() { Success = false };
            }

            if (response.IsSuccessStatusCode)
            {
                var has_data = false;

                T res_data = default(T);

                var res_data_str = JObject.Parse(data)?.ToString();

                if (!string.IsNullOrEmpty(res_data_str))
                {
                    res_data = JsonConvert.DeserializeObject<T>(res_data_str);

                    has_data = true;
                }


                return new Response<T>()
                {
                    Success = true,
                    SuccessWithData = has_data,
                    Data = res_data,
                    Body = JObject.Parse(data),
                    HasData = has_data
                };

            }
            else
            {
                var message = JObject.Parse(data)["message"]?.ToString();

                return new Response<T>()
                {
                    Success = false,
                    ErrorMessage = string.IsNullOrEmpty(message)? "An error has occurred." : message
                };
            }

        }

    }
}

