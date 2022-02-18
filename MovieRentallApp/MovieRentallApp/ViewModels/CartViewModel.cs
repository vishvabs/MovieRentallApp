using MovieRentallApp.Models;
using MovieRentallApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace MovieRentallApp.ViewModels
{
    public class CartViewModel : BaseViewModel
    {

        private CartItem _selectedItem;

        private double total;
        private int selectedDays;

        public ObservableCollection<CartItem> CartMovies { get; }
        public Command LoadCartCommand { get; }

        public Command GetTotal { get; }
        public Command GetRentTotal { get; }
        public Command AddItemCommand { get; }
        public Command<CartItem> DeleteCommand { get; }
        public Command<CartItem> ItemTapped { get; }
        public Command AddOrderCommand { get; }

        public IList<PurchaseType> PurchaseTypes { get { return PurchaseTypeData.purchaseTypes; } }

       


        PurchaseType selectedType;
        public PurchaseType SelectedType
        {
            get { return selectedType; }
            set
            {
                if (SelectedType != value)
                {
                    selectedType = value;
                    OnPropertyChanged();
                }
            }
        }



        public CartViewModel()
        {
            Title = "Cart List";
            CartMovies = new ObservableCollection<CartItem>();
            LoadCartCommand = new Command(async () => await ExecuteLoadMoviesCommand());
            DeleteCommand = new Command<CartItem>(ExecuteDeleteCommand);
            ItemTapped = new Command<CartItem>(OnItemSelected);
            GetTotal = new Command(async () => await ExecuteTotalCommand());
            GetRentTotal = new Command(async () => await ExecuteRentTotalCommand());
            AddItemCommand = new Command(OnAddItem);
            AddOrderCommand = new Command(OnAddOrder);
        }

        private async void OnAddOrder()
        {
           // Item newItem = new Item()
          //  {
          //      Id = Guid.NewGuid().ToString(),
                
         //   };

          //  await DataStore.AddItemAsync(newItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("//MoviesPage");
           
        }

        async Task ExecuteTotalCommand()
        {

            try
            {
                double tot = 0.0;
                foreach (var item in CartMovies)
                {
                   
                   tot += item.Price * item.RunTime;
                }
                this.Total = tot;
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


        async Task ExecuteRentTotalCommand()
        {

            try
            {
                double tot = 0.0;
                foreach (var item in CartMovies)
                {

                    tot += item.Rental * SelectedDays;
                }
                this.Total = tot;
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


        async Task ExecuteLoadMoviesCommand()
        {
            IsBusy = true;

            try
            {
                CartMovies.Clear();
                var items = await CartStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    CartMovies.Add(item);
                    Debug.WriteLine(item.Name);
                }
                if (SelectedType.Val == true)
                {
                    await ExecuteRentTotalCommand();
                }
                else
                {
                    await ExecuteTotalCommand();
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

         async void ExecuteDeleteCommand(CartItem item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            // await Shell.Current.GoToAsync($"{nameof(MovieDetailPage)}?{nameof(MovieDetailViewModel.MovieId)}={item.Id}");
            try
            {
                await CartStore.DeleteItemAsync(item.Id);
                Debug.WriteLine("deleted");
                await ExecuteLoadMoviesCommand();
                if (SelectedType.Val == true)
                {
                    await ExecuteRentTotalCommand();
                }
                else {
                    await ExecuteTotalCommand();
                }
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            //await Shell.Current.GoToAsync(nameof(CartPage));

        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;

        }

        public CartItem SelectedItem
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

        async void OnItemSelected(CartItem item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(MovieDetailPage)}?{nameof(MovieDetailViewModel.MovieId)}={item.Id}");
        }
        public double Total
        {
            get => total;
            set => SetProperty(ref total, value);
        }

        public int SelectedDays
        {
            get => selectedDays;
            set => SetProperty(ref selectedDays, value);
        }


    }
}
