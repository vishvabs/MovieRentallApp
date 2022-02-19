using MovieRentallApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRentallApp.Services
{
    public class MockMovieStore : IDataStore<Movie>
    {
        readonly List<Movie> items;

        public MockMovieStore()
        {
            items = new List<Movie>()
            {
                new Movie { Id = Guid.NewGuid().ToString(), Name = "Titanic", Description="This is an Titanic description.This is an titanic description" , Cast="cast" , Genres="Romantic" , Price=0.20 , ImgUrl="computers.jpg" , RunTime = 72.35 , ReleaseDate = "2021-12-12" , Rental = 20.00  },
                 new Movie { Id = Guid.NewGuid().ToString(), Name = "Fast And Furious F11", Description="Fast And Furious is a amazing action movie. This is an item description" , Cast="cast" , Genres="Action" , Price=0.30 , ImgUrl="imges.jpg" , RunTime = 84.10 , ReleaseDate = "2021-12-12" , Rental = 30.00  },

            };
        }

        public async Task<bool> AddItemAsync(Movie item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Movie arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Movie> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Movie>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }

        public async Task<bool> UpdateItemAsync(Movie item)
        {
            var oldItem = items.Where((Movie arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }
    }
}
