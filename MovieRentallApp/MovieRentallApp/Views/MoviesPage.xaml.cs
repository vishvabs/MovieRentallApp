using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieRentallApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieRentallApp.Views
{
   
    public partial class MoviesPage : ContentPage
    {
        MoviesViewModel _viewModel;
        public MoviesPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new MoviesViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}