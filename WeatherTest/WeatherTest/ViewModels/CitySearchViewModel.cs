using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WeatherTest.Models;
using WeatherTest.Services;
using WeatherTest.Views;
using Xamarin.Forms;

namespace WeatherTest.ViewModels
{
    public class CitySearchViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand SearchCommand { get; set; }

        public string City { get; set; }

        public bool IsBusy { get; set; }
        public string ErrorMessage { get; set; }
        public Action ShowErrorAction { get; set; }
        public Action HideErrorAction { get; set; }


        public CitySearchViewModel()
        {
            OnAppearingEvents();

            SearchCommand = new Command(() => GetData());
        }

        public void OnAppearingEvents()
        {
            if (Application.Current.Properties.ContainsKey("city"))
            {
                City = Application.Current.Properties["city"]?.ToString();
            }
        }


        private async void GetData()
        {
            if(string.IsNullOrWhiteSpace(City))
            {
                DisplayErrorMessage("City is required.");
                return;
            }

            IsBusy = true;

            var response = await HttpService.SendRequest<LocationWeather>(
               httpMethod: System.Net.Http.HttpMethod.Get,
               url: $"https://api.openweathermap.org/data/2.5/weather?q={City}&APPID=d83c5a05c7e33835584ab90d278e5a9d");

            if (response.SuccessWithData)
            {
                var data = response.Data;

                SaveHistoryPopUp();

                await Shell.Current.Navigation.PushAsync(new WeatherDetailsPage(weather: data) { Title = $"{data?.Name}, {data?.Sys?.Country}" });
            }
            else
            {
                if (response.ErrorMessage != null)
                {
                    DisplayErrorMessage(response.ErrorMessage);
                
                }
            }

            IsBusy = false;
        }

        public async void SaveHistoryPopUp()
        {
            if (Application.Current.Properties.ContainsKey("city"))
            {
                var history = Application.Current.Properties["city"]?.ToString();

                if (history == null)
                {
                    return;
                }

                if (history.ToLower() == City.ToLower())
                {
                    return;
                }
            }

            bool answer = await Application.Current.MainPage.DisplayAlert("Question?", "Would you like to play a game", "Yes", "No");

            if(answer)
            {
                Application.Current.Properties["city"] = City;
            }
        }
         
        public void DisplayErrorMessage(string message, double seconds = 2)
        {
            ErrorMessage = message;
            ShowErrorAction();

            Device.StartTimer(TimeSpan.FromSeconds(seconds), () =>
            {
                HideErrorAction();
               // ErrorMessage = "";

                return false; 
            });
        }

        public async void DisplayAlert(string body, string title = "")
        {
            await Application.Current.MainPage.DisplayAlert(title, body, "OK");
        }


        //private Func<bool> CanSearchCommandExecute()
        //{
        //    return new Func<bool>(() => !IsBusy && !string.IsNullOrWhiteSpace(City));
        //}
    }
}

