using ApplicationCore.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Contracts.Repository;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Enums;
using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace Infrastructure.Services
{
    public class GoogleBooksService : IGoogleBooksService
    {
        private readonly HttpClient _client;
        //temporary until I start using keyvault
        private readonly string _apiKey = "AIzaSyCFWJGQseXnIxk6Y5XfMYWX_yhKlAiuMUs";

        public GoogleBooksService(HttpClient client)
        {
            _client = client;
        }

        public async Task<BookDetailsModel> GetBookDetails(int bookId)
        {
            var response = await _client.GetAsync($"https://www.googleapis.com/books/v1/volumes/{bookId}?key={_apiKey}");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<BookDetailsModel>(content);
        }

        public async Task<BookSearchResponseModel> SearchBooks(string query)
        {
            var response = await _client.GetAsync($"https://www.googleapis.com/books/v1/volumes?q={query}&key={_apiKey}");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            dynamic data = JsonConvert.DeserializeObject(content);
            //return data;
            return JsonConvert.DeserializeObject<BookSearchResponseModel>(content);
        }
    }

}
