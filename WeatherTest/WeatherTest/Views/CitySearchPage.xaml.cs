using System;
using System.Collections.Generic;
using WeatherTest.ViewModels;
using Xamarin.Forms;

namespace WeatherTest.Views
{
    public partial class CitySearchPage : ContentPage
    {
        private CitySearchViewModel ViewModel { get; set; }

        public CitySearchPage()
        {
            InitializeComponent();

            ErrorLabel.FadeTo(0, 600);

            ViewModel = new CitySearchViewModel()
            {
                ShowErrorAction = new Action(() => ErrorLabel.FadeTo(1, 600)),
                HideErrorAction = new Action(() => ErrorLabel.FadeTo(0, 600))
            };

            BindingContext = ViewModel;

            Appearing += (o, e) =>
            {
                if(ViewModel != null)
                {
                    ViewModel.OnAppearingEvents();
                }
            };
        }
    }
}

