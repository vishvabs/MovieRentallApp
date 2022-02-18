using MovieRentallApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MovieRentallApp.ViewModels
{
    [QueryProperty(nameof(MovieId), nameof(MovieId))]
    public class MovieDetailViewModel : BaseViewModel
    {
        private string movieId;
        private string name;
        private string description;
        private string genres;
        private string cast;
        private string imgUrl;
        private string releaseDate;
        private double runTime;
        private double price;
        private double rental;
  
        public string Id { get; set; }
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        public IList<PurchaseType>  PurchaseTypes { get { return PurchaseTypeData.purchaseTypes; } }


     

        public MovieDetailViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private async void OnSave()
        {
            CartItem newItem = new CartItem()
            {
                Id = Guid.NewGuid().ToString(),
                Name = Name,
                ImgUrl = ImgUrl,
                Price = Price,
                Rental = Rental,
                RunTime = RunTime,

            };

            Debug.WriteLine(newItem.Name);
            await CartStore.AddItemAsync(newItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(movieId)
                && !String.IsNullOrWhiteSpace(name);
        }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }
        public string Genres
        {
            get => genres;
            set => SetProperty(ref genres, value);
        }
        public string Cast
        {
            get => cast;
            set => SetProperty(ref cast, value);
        }
        public string ImgUrl
        {
            get => imgUrl;
            set => SetProperty(ref imgUrl, value);
        }
        public string ReleaseDate
        {
            get => releaseDate;
            set => SetProperty(ref releaseDate, value);
        }
        public double RunTime
        {
            get => runTime;
            set => SetProperty(ref runTime, value);
        }
        public double Price
        {
            get => price;
            set => SetProperty(ref price, value);
        }
        public double Rental
        {
            get => rental;
            set => SetProperty(ref rental, value);
        }
        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

      

        public string MovieId
        {
            get
            {
                return movieId;
            }
            set
            {
                movieId = value;
                LoadMovieId(value);
            }
        }

        public async void LoadMovieId(string movieId)
        {
            try
            {
                var movie = await MovieStore.GetItemAsync(movieId);
                Id = movie.Id;
                Name = movie.Name;
                Description = movie.Description;
                Cast = movie.Cast;
                Genres = movie.Genres;
                ImgUrl = movie.ImgUrl;
                ReleaseDate = movie.ReleaseDate;
                RunTime = movie.RunTime;
                Price = movie.Price;
                Rental = movie.Rental;
                
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Movie");
            }
        }
    }


}
