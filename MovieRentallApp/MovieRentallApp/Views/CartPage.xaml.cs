using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MovieRentallApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieRentallApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartPage : ContentPage
    {
        CartViewModel _viewModel;
        public CartPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new CartViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        void Handle_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            var selectedOption = (sender as Picker).SelectedItem;
            if (_viewModel.SelectedType.Val == false)
            {
                _viewModel.GetTotal.Execute(selectedOption);

            }
            else if (_viewModel.SelectedType.Val == true)
            {
                _viewModel.GetRentTotal.Execute(selectedOption);
            }
        }

        void Handle_DaysChanged(object sender, System.EventArgs e)
        {
         // int selectedNo = DatePicker.SelectedIndex + 1;
           
            _viewModel.GetRentTotal.Execute(e);

        }

        async void OnButtonClicked(object sender, EventArgs e)
        {
            string address;
            // (sender as Button).Text = "Click me again!";
            if (_viewModel.SelectedType.Val == false)
            {
                address = await DisplayPromptAsync("Order Confirmation", "Please Enter your Email Address?");
            }
            else {
               address = await DisplayPromptAsync("Order Confirmations", "Please Enter your Address?");
            }

            if (address == null || address == "")
            {
                await DisplayAlert("Alert", "PLease Enter Address to Confirm Order", "OK");
            }
            else {
                await DisplayAlert("Success!", "Your Order Placed", "OK");
                _viewModel.AddOrderCommand.Execute(null);
            }
                
        }

    }
}