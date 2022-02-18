using MovieRentallApp.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace MovieRentallApp.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}