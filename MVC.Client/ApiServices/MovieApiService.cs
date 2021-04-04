using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using MVC.Client.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MVC.Client.ApiServices
{
    public class MovieApiService : IMovieApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public MovieApiService(
            IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory ??
            throw new ArgumentNullException(nameof(httpClientFactory));
            httpClient = _httpClientFactory.CreateClient("movieAPIClient");
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IEnumerable<Movie>> GetMovies()
        {
            var request = new HttpRequestMessage(
                HttpMethod.Get,
                "/movies"
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
                "/movies/" + id
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
               HttpMethod.Post,"/movies");

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
              HttpMethod.Put, "/movies");

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
              HttpMethod.Delete, "/movies/" + id);

            var response = await httpClient
                                 .DeleteAsync(request.RequestUri);
            if (response.IsSuccessStatusCode) return;

            throw new Exception(response.StatusCode.ToString());
        }

        public async Task<UserInfoResponse> GetUserInfo()
        {
           var idpClient = _httpClientFactory.CreateClient("IDPClient");

            var disco = await idpClient.GetDiscoveryDocumentAsync();

            if (disco.IsError)
            {
                throw new HttpRequestException("something went wrong connecting to IDP Client");
            }

            var token = await _httpContextAccessor
                .HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
            var userInfo = await idpClient.GetUserInfoAsync(
                new UserInfoRequest
                {
                    Address = disco.UserInfoEndpoint,
                    Token = token
                });
            if(userInfo.IsError)
            {
                throw new HttpRequestException("something went wrong to get a user info");
            }

            return userInfo;
        }
    }
}
