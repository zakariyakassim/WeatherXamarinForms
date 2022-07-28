using System;
using Newtonsoft.Json.Linq;

namespace WeatherTest.Models
{
    public class Response<T>
    {
        public bool Success { get; set; } = false;
        public T Data { get; set; }
        public string ErrorMessage { get; set; }
        public JObject Body { get; set; }
        public bool HasData { get; set; }
        public bool SuccessWithData { get; set; }
    }
}

