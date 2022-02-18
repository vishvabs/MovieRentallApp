using MovieRentallApp.Models;
using MovieRentallApp.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MovieRentallApp.ViewModels
{
    public class MoviesViewModel : BaseViewModel
    {
        private Movie _selectedItem;

        public ObservableCollection<Movie> Movies { get; }
        public Command LoadMoviesCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Movie> ItemTapped { get; }

        public MoviesViewModel()
        {
            Title = "Movie List";
            Movies = new ObservableCollection<Movie>();
            LoadMoviesCommand = new Command(async () => await ExecuteLoadMoviesCommand());

            ItemTapped = new Command<Movie>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);
        }

        async Task ExecuteLoadMoviesCommand()
        {
            IsBusy = true;

            try
            {
                Movies.Clear();
                var items = await MovieStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Movies.Add(item);
                    Debug.WriteLine(item.Name);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public Movie SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        async void OnItemSelected(Movie item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(MovieDetailPage)}?{nameof(MovieDetailViewModel.MovieId)}={item.Id}");
        }
    }
}
