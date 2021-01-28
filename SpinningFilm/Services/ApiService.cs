using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SpinningFilm.Helpers;
using SpinningFilm.Interfaces;

namespace SpinningFilm.Services
{
    public class ApiService : IApiService
    {
        private readonly ApiSettings _apiSettings;

        public ApiService(IOptions<ApiSettings> apiSettings)
        {
            _apiSettings = apiSettings.Value;
        }

        public string GetTmdbKey()
        {
            return _apiSettings.TmdbKey;
        }

        public string GetOmdbKey()
        {
            return _apiSettings.OmdbKey;
        }

        public async Task<string> GetOmdbResult(string imdbId)
        {
            string key = GetOmdbKey();
            string url = string.Format("http://www.omdbapi.com/?apikey={0}&i={1}", key, imdbId);
            return await HttpGet(url);
        }

        public async Task<string> SearchAsync(string term)
        {
            string key = GetTmdbKey();
            string url = string.Format("https://api.themoviedb.org/3/search/multi?api_key={0}&query={1}", key, term);
            return await HttpGet(url);
        }

        public async Task<string> GetWithExternalIdsAsync(string tmdbId, string type)
        {
            string key = GetTmdbKey();
            string url = string.Format("https://api.themoviedb.org/3/{0}/{1}?api_key={2}&append_to_response=external_ids", type, tmdbId, key);
            return await HttpGet(url);
        }

        public async Task<string> GetWithCreditsAsync(string tmdbId, string type)
        {
            string key = GetTmdbKey();
            string url = string.Format("https://api.themoviedb.org/3/{2}/{0}?api_key={1}&append_to_response=credits", tmdbId, key, type);
            return await HttpGet(url);
        }

        public async Task<string> HttpGet(string url)
        {
            HttpClient http = new HttpClient();
            var response = await http.GetAsync(url);
            var responseStatusCode = response.StatusCode;
            var responseBody = await response.Content.ReadAsStringAsync();

            return responseBody;
        }
    }
}
