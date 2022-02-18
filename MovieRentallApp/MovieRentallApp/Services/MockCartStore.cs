using MovieRentallApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRentallApp.Services
{
    public class MockCartStore : IDataStore<CartItem>
    {

        readonly List<CartItem> cart;

        public MockCartStore()
        {
            cart = new List<CartItem>()
            {
              

            };
        }

        public async Task<bool> AddItemAsync(CartItem item)
        {
            Debug.WriteLine(item);
            cart.Add(item);
            
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = cart.Where((CartItem arg) => arg.Id == id).FirstOrDefault();
            cart.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<CartItem> GetItemAsync(string id)
        {
            return await Task.FromResult(cart.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<CartItem>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(cart);
        }

        public async Task<bool> UpdateItemAsync(CartItem item)
        {
            var oldItem = cart.Where((CartItem arg) => arg.Id == item.Id).FirstOrDefault();
            cart.Remove(oldItem);
            cart.Add(item);

            return await Task.FromResult(true);
        }
    }
}
