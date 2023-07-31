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

        public async Task<BookSearchResponseModel> SearchBooks(string query, int startIndex = 0, int maxResults = 10)
        {
            var response = await _client.GetAsync($"https://www.googleapis.com/books/v1/volumes?q={query}&startIndex={startIndex}&maxResults={maxResults}&key={_apiKey}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            //return data;
            return JsonConvert.DeserializeObject<BookSearchResponseModel>(content);
        }
    }

}
