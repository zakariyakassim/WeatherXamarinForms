using System;
using System.Collections.Generic;
using WeatherTest.Models;
using WeatherTest.ViewModels;
using Xamarin.Forms;

namespace WeatherTest.Views
{
    public partial class WeatherDetailsPage : ContentPage
    {
        public WeatherDetailsPage(LocationWeather weather)
        {
            InitializeComponent();

            BindingContext = new WeatherDetailsViewModel(data: weather);
        }
    }
}

