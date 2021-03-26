using MVC.Client.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC.Client.ApiServices
{
    public class MovieApiService : IMovieApiService
    {
        public async Task<IEnumerable<Movie>> GetMovies()
        {
            var movieList = new List<Movie>
            {
                new Movie
                {
                    Id = 1,
                    Genre = "Comic",
                    Title = "Adbs",
                    Rating = "9.2",
                    ImageUrl = "image/src",
                    ReleaseDate = DateTime.Now,
                    Owner = "agaba"
                }
            };
            return await Task.FromResult(movieList);
        }

        public Task<Movie> GetMovie(int id)
        {
            throw new NotImplementedException();
        }
        public Task<Movie> CreateMovie(Movie movie)
        {
            throw new NotImplementedException();
        }

        public Task<Movie> UpdateMovie(Movie movie)
        {
            throw new NotImplementedException();
        }

        public Task DeleteMovie(int id)
        {
            throw new NotImplementedException();
        }
    }
}
