using System;
using System.ComponentModel;
using WeatherTest.Models;

namespace WeatherTest.ViewModels
{
    public class WeatherDetailsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Description { get; set; }
        public string Temperature { get; set; }
        public string IconSource { get; set; }

        public string FavButtonTitle { get; set; }

        public WeatherDetailsViewModel(LocationWeather data)
        {
            var weather = data?.Weather[0];

            if(weather != null)
            {
                Description = data?.Weather[0]?.Description;

                var celcius = Helpers.KelvinToCelsius(data.Main.Temp);

                Temperature = $"{celcius}°C";

                IconSource = $"https://openweathermap.org/img/wn/{weather?.Icon}@2x.png";
            }


        }
    }
}

