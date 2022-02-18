using MovieRentallApp.Services;
using MovieRentallApp.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieRentallApp
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            DependencyService.Register<MockMovieStore>();
            DependencyService.Register<MockCartStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
