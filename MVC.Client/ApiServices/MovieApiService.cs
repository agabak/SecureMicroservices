using IdentityModel.Client;
using MVC.Client.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MVC.Client.ApiServices
{
    public class MovieApiService : IMovieApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient httpClient;
        public MovieApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory ??
            throw new ArgumentNullException(nameof(httpClientFactory));
            httpClient = _httpClientFactory.CreateClient("movieAPIClient");
        }
        public async Task<IEnumerable<Movie>> GetMovies()
        {
            var request = new HttpRequestMessage(
                HttpMethod.Get,
                "api/movies"
                );
            var response = await httpClient.SendAsync(
                request,HttpCompletionOption.ResponseHeadersRead
                ).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<Movie>>(content);
        }

        public async Task<Movie> GetMovie(int id)
        {
            var request = new HttpRequestMessage(
                HttpMethod.Get,
                "api/movies/" + id
                );
            var response = await httpClient.SendAsync(
                request, HttpCompletionOption.ResponseHeadersRead
                ).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Movie>(content);
        }

        public async Task<Movie> CreateMovie(Movie movie)
        {
            var request = new HttpRequestMessage(
               HttpMethod.Post,"api/movies");

            var content = JsonConvert.SerializeObject(movie);
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);

            byteContent.Headers.ContentType = 
                new MediaTypeHeaderValue("application/json");

            var response = await httpClient
                                 .PostAsync(request.RequestUri, byteContent);
            if(response.IsSuccessStatusCode) return movie;
            
            throw new Exception(response.StatusCode.ToString());
        }

        public async Task<Movie> UpdateMovie(Movie movie)
        {
            var request = new HttpRequestMessage(
              HttpMethod.Put, "api/movies");

            var content = JsonConvert.SerializeObject(movie);
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);

            byteContent.Headers.ContentType =
                new MediaTypeHeaderValue("application/json");

            var response = await httpClient
                                 .PutAsync(request.RequestUri, byteContent);
            if (response.IsSuccessStatusCode) return movie;

            throw new Exception(response.StatusCode.ToString());
        }

        public async Task DeleteMovie(int id)
        {
            var request = new HttpRequestMessage(
              HttpMethod.Delete, "api/movies/" + id);

            var response = await httpClient
                                 .DeleteAsync(request.RequestUri);
            if (response.IsSuccessStatusCode) return;

            throw new Exception(response.StatusCode.ToString());
        }
    }
}
